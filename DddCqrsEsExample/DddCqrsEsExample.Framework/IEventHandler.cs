namespace DddCqrsEsExample.Framework
{
    public interface IEventHandler<TEvent>
    {
        void Handle(TEvent evt);
    }
}