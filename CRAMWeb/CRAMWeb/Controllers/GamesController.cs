using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRAMWeb.Models;
using Microsoft.AspNet.Identity;
using CRAMWeb.ContextManagers;

namespace CRAMWeb.Controllers
{
    public class GamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Gets User Game Ids
        /// </summary>
        /// <param name="games"></param>
        /// <returns></returns>
        private List<int> GetUserGames(List<Game> games)
        {
            if (!User.Identity.IsAuthenticated) return null;

            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            return games.Where(g => g.Users.Contains(user)).Select(g => g.Id).ToList();
        }
        
        // GET: Game
        public ActionResult Index()
        {
            var games = db.Games.ToList();
            ViewBag.UserGames = GetUserGames(games);

            return View(games);
        }

        // GET: Games/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            var games = new List<Game> { game };
            ViewBag.UserGames = GetUserGames(games);

            return View(game);
        }

        // GET: Games/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,GameName,MaxPlayers")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(game);
        }

        // GET: Games/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,GameName,MaxPlayers")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Games/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Games/Join/5
        [HttpGet, ActionName("Join")]
        [Authorize]
        public ActionResult JoinGame(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                GameContextManager.AddOrUpdateUserAndGameConnection(db, id, userId);
                return RedirectToAction("Details", new { id });
            }
            return RedirectToAction("Details", new {id});
        }

        // GET: Games/Leave/5
        [HttpGet, ActionName("Leave")]
        [Authorize]
        public ActionResult LeaveGame(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                GameContextManager.AddOrUpdateUserAndGameConnection(db, id, userId);
                return RedirectToAction("Details", new { id });
            }
            return RedirectToAction("Details", new { id });
        }

        // GET: Games/Start/5
        [HttpGet, ActionName("Start")]
        [Authorize]
        public ActionResult StartGame(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                Game game = db.Games.Single(g => g.Id == id);
                if(!game.IsStarted)
                {
                    game.IsStarted = true;
                    foreach (var user in game.Users)
                    {
                        GameState gameState = new GameState()
                        {
                            Food = 0,
                            Wood = 0,
                            Stone = 0,
                            Gold = 0,
                            Soldiers = 0,
                            CastleLevel = 0,
                            FarmsLevel = 0,
                            LumberjackLevel = 0,
                            HousingLevel = 0,
                            MinesLevel = 0,
                            User = user,
                            Game = game,
                            SentRaids = new List<Raid>(),
                            ReceivedRaids = new List<Raid>()
                        };
                        db.GameStates.Add(gameState);
                        db.SaveChanges();
                    }
                }
                GameState userGameState = game.GameStates.Single(g => g.User.Id == userId);
                return RedirectToAction("Game", "Home", new { userGameState.Id });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
