using System.Collections.Generic;
using Castle.Windsor;
using DddCqrsEsExample.Domain.Orders;
using DddCqrsEsExample.Framework;

namespace DddCqrsEsExample.ApplicationServices.Orders
{
    public class AddItemsToSalesOrderCommandHandler : CommandHandlerBase<AddItemsToSalesOrderCommand>
    {
        private readonly IWindsorContainer _container;
        private readonly IEventBus _eventBus;

        public AddItemsToSalesOrderCommandHandler(IWindsorContainer container, IEventBus eventBus)
        {
            _container = container;
            _eventBus = eventBus;
        }

        public override void Handle(AddItemsToSalesOrderCommand command)
        {
            var repository = _container.Resolve<IRepository<SalesOrder>>();
            try
            {
                var order = repository.Get(command.Id);
                order.AddItem(command.Sku, command.Quantity, command.UnitPrice);
                IEnumerable<Event> events = order.GetUncommittedEvents();
                repository.Save(order);
                order.AcceptUncommittedEvents();
                _eventBus.Send(events);
            }
            finally
            {
                _container.Release(repository);
            }
        }
    }
}