using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CRAMWeb.Models;

namespace CRAMWeb.Controllers
{
    public class GameStateController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/GameState
        public IQueryable<GameState> GetGameStates()
        {
            return db.GameStates;
        }

        // GET: api/GameState/5
        [ResponseType(typeof(GameStateDTO))]
        public IHttpActionResult GetGameState(int id)
        {
            GameState gameState = db.GameStates.Find(id);
            if (gameState == null)
            {
                return NotFound();
            }

            GameStateDTO gameStateDTO = new GameStateDTO {
                Id = gameState.Id,
                Food = gameState.Food,
                Wood = gameState.Wood,
                Stone = gameState.Stone,
                Gold = gameState.Gold,
            };

            return Ok(gameStateDTO);
        }

        // PUT: api/GameState/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGameState(int id, GameState gameState)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gameState.Id)
            {
                return BadRequest();
            }

            db.Entry(gameState).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameStateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/GameState
        [ResponseType(typeof(GameState))]
        public IHttpActionResult PostGameState(GameState gameState)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GameStates.Add(gameState);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = gameState.Id }, gameState);
        }

        // DELETE: api/GameState/5
        [ResponseType(typeof(GameState))]
        public IHttpActionResult DeleteGameState(int id)
        {
            GameState gameState = db.GameStates.Find(id);
            if (gameState == null)
            {
                return NotFound();
            }

            db.GameStates.Remove(gameState);
            db.SaveChanges();

            return Ok(gameState);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameStateExists(int id)
        {
            return db.GameStates.Count(e => e.Id == id) > 0;
        }
    }
}