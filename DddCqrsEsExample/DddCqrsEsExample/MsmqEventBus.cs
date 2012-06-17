using System.Collections.Generic;
using System.Messaging;
using DddCqrsEsExample.Framework;
using Newtonsoft.Json;

namespace DddCqrsEsExample
{
    public class MsmqEventBus : IEventBus
    {
        private const string QueueName = @".\private$\DddCqrsEsExample";

        public void Send<TEvent>(TEvent evt) where TEvent : Event
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

        public void Send<TEvent>(IEnumerable<TEvent> events) where TEvent : Event
        {
            foreach (var evt in events)
            {
                Send(evt);
            }
        }
    }
}