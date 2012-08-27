using System.Web.Mvc;
using DddCqrsExample.Web.Models.Shopping;

namespace DddCqrsExample.Web.Controllers
{
    public class BasketController : Controller
    {
        public ActionResult Index()
        {
            var basket = HttpContext.Cache.Get("basket") as Basket ?? new Basket();

            return View(basket.GetItems());
        }
    }
}
