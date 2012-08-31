using System.Collections.Generic;

namespace DddCqrsEsExample.Framework
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent evt) where TEvent : Event;
        void Publish<TEvent>(IEnumerable<TEvent> evt) where TEvent : Event;
    }
}