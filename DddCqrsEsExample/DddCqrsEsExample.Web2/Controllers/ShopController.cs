using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;
using System.Web.Mvc;
using Dapper;
using DddCqrsEsExample.Domain;
using DddCqrsEsExample.Domain.Orders;
using DddCqrsEsExample.Framework;
using DddCqrsEsExample.Web2.Infrastructure;
using DddCqrsEsExample.Web2.Models.Products;
using DddCqrsEsExample.Web2.Models.Shopping;
using Inforigami.Regalo.Core;

namespace DddCqrsEsExample.Web2.Controllers
{
    public class ShopController : Controller
    {
        private readonly IReadStoreConnectionFactory _readStoreConnectionFactory;
        private readonly ICommandProcessor _commandProcessor;
        private readonly ILogger _logger;

        public ShopController(
            IReadStoreConnectionFactory readStoreConnectionFactory,
            ICommandProcessor commandProcessor,
            ILogger logger)
        {
            if (readStoreConnectionFactory == null) throw new ArgumentNullException(nameof(readStoreConnectionFactory));
            if (commandProcessor == null) throw new ArgumentNullException(nameof(commandProcessor));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _readStoreConnectionFactory = readStoreConnectionFactory;
            _commandProcessor = commandProcessor;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            using (var readStore = _readStoreConnectionFactory.Create())
            {
                return View(readStore.Query<ProductListItemViewModel>("SELECT Id, Description, Amount as Price FROM Product"));
            }
        }

        [HttpGet]
        public ActionResult AddToBasket(string id)
        {
            using (var readStore = _readStoreConnectionFactory.Create())
            {
                var vm =
                    readStore.Query<ProductListItemViewModel>(
                        "SELECT Id, Description, Amount as Price FROM Product WHERE Id = @Id",
                        new {Id = id})
                             .Select(
                                 x =>
                                     new AddToBasketViewModel
                                     {
                                         Id = x.Id,
                                         Description = x.Description,
                                         Price = x.Price,
                                         Quantity = 1
                                     })
                             .Single();

                return View(vm);
            }
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
                _commandProcessor.Process(new AddItemsToSalesOrderCommand(createSalesOrderCommand.Id, new Sku(item.Description), item.Quantity, new Money(item.Price, Currency.GBP)));
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

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            _logger.Error(this, filterContext.Exception, "Failed!");

            TempData.Add("message", filterContext.Exception.Message);
            filterContext.Result = View("Fail");
            filterContext.ExceptionHandled = true;
        }
    }
}