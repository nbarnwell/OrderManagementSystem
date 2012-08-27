using System.Collections.Generic;

using DddCqrsEsExample.Domain.Orders;
using DddCqrsEsExample.Framework;

namespace DddCqrsEsExample.ApplicationServices.Orders
{
    using System;

    public class AddItemsToSalesOrderCommandHandler : CommandHandlerBase<AddItemsToSalesOrderCommand>
    {
        private readonly IEventBus _eventBus;

        private readonly IRepository<SalesOrder> _salesOrderRepository;

        public AddItemsToSalesOrderCommandHandler(IEventBus eventBus, IRepository<SalesOrder> salesOrderRepository)
        {
            if (eventBus == null)
            {
                throw new ArgumentNullException("eventBus");
            }
            if (salesOrderRepository == null)
            {
                throw new ArgumentNullException("salesOrderRepository");
            }

            _eventBus = eventBus;
            _salesOrderRepository = salesOrderRepository;
        }

        public override void Handle(AddItemsToSalesOrderCommand command)
        {
            var order = _salesOrderRepository.Get(command.Id);

            order.AddItem(command.Sku, command.Quantity, command.UnitPrice);

            IEnumerable<Event> events = order.GetUncommittedEvents();

            _salesOrderRepository.Save(order);

            order.AcceptUncommittedEvents();

            _eventBus.Send(events);
        }
    }
}