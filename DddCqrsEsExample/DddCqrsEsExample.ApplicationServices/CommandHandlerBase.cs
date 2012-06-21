using DddCqrsEsExample.Framework;

namespace DddCqrsEsExample.ApplicationServices
{
    public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand>
    {
        public abstract void Handle(TCommand command);
    }
}