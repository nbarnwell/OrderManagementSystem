namespace DddCqrsEsExample.Framework
{
    public interface ICommandProcessor
    {
        void Process<TCommand>(TCommand command);
    }
}