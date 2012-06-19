using System.Collections.Generic;
using Castle.Windsor;
using DddCqrsExample.Domain.Orders;
using DddCqrsExample.Framework;

namespace DddCqrsExample.ApplicationServices.Orders
{
    public class CreateSalesOrderCommandHandler : ICommandHandler<CreateSalesOrderCommand>
    {
        private readonly IWindsorContainer _container;
        private readonly IEventBus _eventBus;

        public CreateSalesOrderCommandHandler(IWindsorContainer container, IEventBus eventBus)
        {
            _container = container;
            _eventBus = eventBus;
        }

        public void Handle(CreateSalesOrderCommand command)
        {
            var repository = _container.Resolve<IRepository<SalesOrder>>();
            try
            {
                var order = new SalesOrder();
                order.Create(command.Id, command.MaxValue);
                IEnumerable<Event> events = order.GetUncommittedEvents();
                repository.Save(order);
                order.AcceptUncommittedEvents();
                _eventBus.Publish(events);
            }
            finally
            {
                _container.Release(repository);
            }
        }
    }
}