using System.Collections.Generic;
using System.Messaging;
using DddCqrsExample.Framework;
using Newtonsoft.Json;

namespace DddCqrsExample
{
    public class MsmqEventBus : IEventBus
    {
        private const string QueueName = @".\private$\DddCqrsExample";

        public void Publish<TEvent>(TEvent evt) where TEvent : Event
        {
            if (!MessageQueue.Exists(QueueName))
            {
                MessageQueue.Create(QueueName);
            }

            using (var q = new MessageQueue(QueueName))
            {
                q.DefaultPropertiesToSend.Recoverable = true;

                q.Send(JsonConvert.SerializeObject(evt, Formatting.Indented) + "|" + evt.GetType().AssemblyQualifiedName);
            }
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