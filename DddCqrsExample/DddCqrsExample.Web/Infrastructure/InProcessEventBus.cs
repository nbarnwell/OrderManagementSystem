using System.Collections.Generic;
using DddCqrsExample.Framework;
using DddCqrsExample.ThinReadLayer.Core;

namespace DddCqrsExample.Web.Infrastructure
{
    public class InProcessEventBus : IEventBus
    {
        public void Publish<TEvent>(TEvent evt) where TEvent : Event
        {
            new Denormaliser().StoreEvent(evt);
        }

        public void Publish<TEvent>(IEnumerable<TEvent> events) where TEvent : Event
        {
            foreach (var evt in events)
            {
                Publish(evt);
            }
        }
    }
}