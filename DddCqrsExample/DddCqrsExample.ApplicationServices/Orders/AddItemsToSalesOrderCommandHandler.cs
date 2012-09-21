using System;
using System.Collections.Generic;

using DddCqrsExample.Domain.Orders;
using DddCqrsExample.Framework;

namespace DddCqrsExample.ApplicationServices.Orders
{
    public class AddItemsToSalesOrderCommandHandler : CommandHandlerBase<AddItemsToSalesOrderCommand>
    {
        private readonly IEventBus _eventBus;

        private readonly IRepository<SalesOrder> _salesOrderRepository;

        public AddItemsToSalesOrderCommandHandler(IEventBus eventBus, IRepository<SalesOrder> salesOrderRepository)
        {
            if (eventBus == null) throw new ArgumentNullException("eventBus");
            if (salesOrderRepository == null) throw new ArgumentNullException("salesOrderRepository");

            _eventBus = eventBus;
            _salesOrderRepository = salesOrderRepository;
        }

        public override void Handle(AddItemsToSalesOrderCommand command)
        {
            var order = _salesOrderRepository.Get(command.Id);
            
            order.AddItem(command.Sku, command.Quantity, command.UnitPrice);
            
            _salesOrderRepository.Save(order);
            
            _eventBus.Publish(
                new ItemsAddedToSalesOrderEvent(
                    order.Id, command.Sku, command.Quantity, command.UnitPrice, DateTimeOffset.Now));
        }
    }
}