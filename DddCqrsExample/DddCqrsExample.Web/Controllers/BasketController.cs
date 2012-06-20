using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DddCqrsExample.Web.Models.Shopping;

namespace DddCqrsExample.Web.Controllers
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
