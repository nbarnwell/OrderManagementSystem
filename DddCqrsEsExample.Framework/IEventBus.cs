using System.Collections.Generic;

namespace DddCqrsEsExample.Framework
{
    public interface IEventBus
    {
        void Send<TEvent>(TEvent evt) where TEvent : Event;
        void Send<TEvent>(IEnumerable<TEvent> evt) where TEvent : Event;
    }
}