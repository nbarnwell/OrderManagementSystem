using System;
using System.Collections.Generic;
using System.Messaging;
using Castle.Windsor;
using DddCqrsEsExample.Framework;
using Newtonsoft.Json;

namespace DddCqrsEsExample.Web2.Infrastructure
{
    public class MsmqEventBus : IEventBus
    {
        private readonly IWindsorContainer _container;

        private const string QueueName = @".\private$\DddCqrsEsExample";

        public MsmqEventBus(IWindsorContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            _container = container;
        }

        public void Publish<TEvent>(TEvent evt) where TEvent : Event
        {
            HandleEvent(evt);

            PublishOnMsmq(evt);
        }

        private void HandleEvent<TEvent>(TEvent evt) where TEvent : Event
        {
            var handlerType = typeof(IEventHandler<>).MakeGenericType(evt.GetType());
            var handlers = _container.ResolveAll(handlerType);
            foreach (var handler in handlers)
            {
                try
                {
                    var handleMethod = handler.GetType().GetMethod("Handle");
                    handleMethod.Invoke(handler, new object[] {evt});
                }
                finally
                {
                    _container.Release(handler);
                }
            }
        }

        private static void PublishOnMsmq<TEvent>(TEvent evt) where TEvent : Event
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