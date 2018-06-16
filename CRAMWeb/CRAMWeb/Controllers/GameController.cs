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
            return View();
        }
    }
}