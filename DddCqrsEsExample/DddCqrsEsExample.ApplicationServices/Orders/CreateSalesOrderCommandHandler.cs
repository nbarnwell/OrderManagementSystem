using System.Collections.Generic;
using Castle.Windsor;
using DddCqrsEsExample.Domain.Orders;
using DddCqrsEsExample.Framework;

namespace DddCqrsEsExample.ApplicationServices.Orders
{
    public class CreateSalesOrderCommandHandler : CommandHandlerBase<CreateSalesOrderCommand>
    {
        private readonly IWindsorContainer _container;
        private readonly IEventBus _eventBus;

        public CreateSalesOrderCommandHandler(IWindsorContainer container, IEventBus eventBus)
        {
            _container = container;
            _eventBus = eventBus;
        }

        public override void Handle(CreateSalesOrderCommand command)
        {
            var repository = _container.Resolve<IRepository<SalesOrder>>();
            try
            {
                var order = new SalesOrder();
                order.Create(command.Id, command.MaxValue);
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