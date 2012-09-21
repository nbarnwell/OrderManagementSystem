using System;
using System.Collections.Generic;

using DddCqrsExample.Domain.Orders;
using DddCqrsExample.Framework;

namespace DddCqrsExample.ApplicationServices.Orders
{
    public class CreateSalesOrderCommandHandler : CommandHandlerBase<CreateSalesOrderCommand>
    {
        private readonly IEventBus _eventBus;

        private readonly IRepository<SalesOrder> _salesOrderRepository;

        public CreateSalesOrderCommandHandler(IEventBus eventBus, IRepository<SalesOrder> salesOrderRepository)
        {
            if (eventBus == null) throw new ArgumentNullException("eventBus");
            if (salesOrderRepository == null) throw new ArgumentNullException("salesOrderRepository");

            _eventBus = eventBus;
            _salesOrderRepository = salesOrderRepository;
        }

        public override void Handle(CreateSalesOrderCommand command)
        {
            var order = new SalesOrder();
            
            order.Create(command.Id, command.MaxValue);
            
            _salesOrderRepository.Save(order);

            _eventBus.Publish(new SalesOrderCreatedEvent(command.Id, command.MaxValue, DateTimeOffset.Now));
        }
    }
}