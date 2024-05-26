using System.Web.Mvc;

namespace PawsAndEars.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}