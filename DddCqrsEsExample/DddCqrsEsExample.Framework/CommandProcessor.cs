using Castle.Windsor;

namespace DddCqrsEsExample.Framework
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IWindsorContainer _container;

        public CommandProcessor(IWindsorContainer container)
        {
            _container = container;
        }

        public void Process<TCommand>(TCommand command)
        {
            var handler = _container.Resolve<ICommandHandler<TCommand>>();
            try
            {
                handler.Handle(command);
            }
            finally
            {
                _container.Release(handler);
            }
        }
    }
}