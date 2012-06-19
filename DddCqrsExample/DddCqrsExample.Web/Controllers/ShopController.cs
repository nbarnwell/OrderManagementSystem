using System.Web.Mvc;

namespace DddCqrsExample.Web.Controllers
{
    public class ShopController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}