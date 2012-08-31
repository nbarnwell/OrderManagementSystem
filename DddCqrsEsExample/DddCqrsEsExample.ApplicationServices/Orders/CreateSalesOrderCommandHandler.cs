using System.Collections.Generic;

using DddCqrsEsExample.Domain.Orders;
using DddCqrsEsExample.Framework;

namespace DddCqrsEsExample.ApplicationServices.Orders
{
    using System;

    public class CreateSalesOrderCommandHandler : CommandHandlerBase<CreateSalesOrderCommand>
    {
        private readonly IEventBus _eventBus;

        private readonly IRepository<SalesOrder> _salesOrderRepository;

        public CreateSalesOrderCommandHandler(IEventBus eventBus, IRepository<SalesOrder> salesOrderRepository)
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

        public override void Handle(CreateSalesOrderCommand command)
        {
            var order = new SalesOrder();
            
            order.Create(command.Id, command.MaxValue);
            
            IEnumerable<Event> events = order.GetUncommittedEvents();
            
            _salesOrderRepository.Save(order);
            
            order.AcceptUncommittedEvents();
            
            _eventBus.Publish(events);
        }
    }
}