namespace DddCqrsEsExample.Framework
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }
}