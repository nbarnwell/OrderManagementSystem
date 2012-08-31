using System.Collections.Generic;

namespace DddCqrsExample.Framework
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent evt) where TEvent : Event;
        void Publish<TEvent>(IEnumerable<TEvent> events) where TEvent : Event;
    }
}