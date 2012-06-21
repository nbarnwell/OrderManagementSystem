using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;
using System.Web.Mvc;
using Dapper;
using DddCqrsExample.ApplicationServices.Orders;
using DddCqrsExample.Domain;
using DddCqrsExample.Domain.Orders;
using DddCqrsExample.Framework;
using DddCqrsExample.Web.Intrastructure;
using DddCqrsExample.Web.Models.Products;
using DddCqrsExample.Web.Models.Shopping;

namespace DddCqrsExample.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IReadStoreConnectionFactory _readStore;
        private readonly ICommandProcessor _commandProcessor;

        public ShopController(IReadStoreConnectionFactory readStore, ICommandProcessor commandProcessor)
        {
            _readStore = readStore;
            _commandProcessor = commandProcessor;
        }

        [HttpGet]
        public ActionResult Index()
        {
            using (var readStore = _readStore.Create())
            {
                return View(readStore.Query<ProductListItemViewModel>("SELECT Id, Description FROM Product"));
            }
        }

        [HttpGet]
        public ActionResult AddToBasket(string id)
        {
            string description;
            using (var readStore = _readStore.Create())
            {
                IEnumerable<ProductListItemViewModel> productList = readStore.Query<ProductListItemViewModel>("SELECT Id, Description FROM Product WHERE Id = @Id", new { Id = id });
                description = productList.Single().Description;
            }

            return View(new AddToBasketViewModel {Id = id, Description = description });
        }

        [HttpPost]
        public ActionResult AddToBasket(AddToBasketViewModel model)
        {
            var basket = HttpContext.Cache.Get("basket") as Basket;

            if (basket == null)
            {
                basket = new Basket();
                HttpContext.Cache.Add("basket", basket, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }

            basket.AddItem(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PlaceOrder()
        {
            var basket = HttpContext.Cache.Get("basket") as Basket;

            if (basket == null)
            {
                TempData.Add("message", "You have nothing in your basket");
                return View("Fail");
            }

            var createSalesOrderCommand = new CreateSalesOrderCommand(new Money((decimal)7.5, Currency.GBP));
            _commandProcessor.Process(createSalesOrderCommand);

            foreach (var item in basket.GetItems())
            {
                _commandProcessor.Process(new AddItemsToSalesOrderCommand(createSalesOrderCommand.Id, new Sku(item.Description), item.Quantity, new Money((decimal)1.25, Currency.GBP)));
            }

            basket.Clear();
            TempData.Add("message", "Thank you for your order. :)");
            return View("Win");
        }

        [HttpPost]
        public ActionResult EmptyBasket()
        {
            var basket = HttpContext.Cache.Get("basket") as Basket;

            if (basket == null)
            {
                TempData.Add("message", "Your basket was already empty.");
                return View("Fail");
            }

            basket.Clear();
            return RedirectToAction("Index");
        }
    }
}