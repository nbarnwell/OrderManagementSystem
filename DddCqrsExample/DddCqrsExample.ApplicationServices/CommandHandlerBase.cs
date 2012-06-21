using DddCqrsExample.Framework;

namespace DddCqrsExample.ApplicationServices
{
    public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand>
    {
        public abstract void Handle(TCommand command);
    }
}