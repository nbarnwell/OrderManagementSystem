using System.Web.Mvc;

namespace DddCqrsEsExample.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to The DDD + CQRS + ES Demo App!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
