namespace DddCqrsExample.Framework
{
    public interface ICommandProcessor
    {
        void Process<TCommand>(TCommand command);
    }
}