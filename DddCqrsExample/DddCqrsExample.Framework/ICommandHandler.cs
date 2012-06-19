namespace DddCqrsExample.Framework
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }
}