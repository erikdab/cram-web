using CRAMWeb.Models;
using System.Linq;
using System.Web.Mvc;

namespace CRAMWeb.Controllers
{
    /// <summary>
    /// MVC Controller for the Game
    /// </summary>
    [Authorize]
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {
                var users = db.Users.ToList();
            }
            return View();
        }
    }
}