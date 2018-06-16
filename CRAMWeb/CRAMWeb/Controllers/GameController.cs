using CRAMWeb.Models;
using System.Linq;
using System.Web.Mvc;

namespace CRAMWeb.Controllers
{
    /// <summary>
    /// MVC Controller for the Game
    /// </summary>
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            using (var db = new ApplicationContext())
            {
                var users = db.Users.ToList();
            }
            return View();
        }
    }
}