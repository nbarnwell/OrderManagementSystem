using System.Web.Mvc;
using DddCqrsEsExample.Web2.Models.Shopping;

namespace DddCqrsEsExample.Web2.Controllers
{
    public class BasketController : Controller
    {
        public ActionResult Index()
        {
            var basket = HttpContext.Cache.Get("basket") as Basket;
            if (basket == null) basket = new Basket();

            return View(basket.GetItems());
        }
    }
}
