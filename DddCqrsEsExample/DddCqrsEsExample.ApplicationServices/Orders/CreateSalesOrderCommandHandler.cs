using System.Collections.Generic;

using DddCqrsEsExample.Domain.Orders;
using DddCqrsEsExample.Framework;
using Inforigami.Regalo.Core;

namespace DddCqrsEsExample.ApplicationServices.Orders
{
    using System;

    public class CreateSalesOrderCommandHandler : CommandHandlerBase<CreateSalesOrderCommand>
    {
        private readonly IEventBus _eventBus;
        private readonly IRepository<SalesOrder> _salesOrderRepository;
        private readonly ILogger _logger;

        public CreateSalesOrderCommandHandler(IEventBus eventBus, IRepository<SalesOrder> salesOrderRepository, ILogger logger)
        {
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));
            if (salesOrderRepository == null) throw new ArgumentNullException(nameof(salesOrderRepository));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _eventBus = eventBus;
            _salesOrderRepository = salesOrderRepository;
            _logger = logger;
        }

        public override void Handle(CreateSalesOrderCommand command)
        {
            var order = new SalesOrder();
            
            order.Create(command.Id, command.MaxValue);
            
            IEnumerable<Event> events = order.GetUncommittedEvents();
            
            _salesOrderRepository.Save(order);
            
            order.AcceptUncommittedEvents();
            
            _eventBus.Publish(events);

            _logger.Info(this, $"SalesOrder created: {command.Id}");
        }
    }
}