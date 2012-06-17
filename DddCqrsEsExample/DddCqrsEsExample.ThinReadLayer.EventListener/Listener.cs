using System;
using System.Messaging;
using DddCqrsEsExample.Framework;
using Newtonsoft.Json;

namespace DddCqrsEsExample.ThinReadLayer.EventListener
{
    public class Listener
    {
        private const string QueueName = @".\private$\DddCqrsEsExample";
        private MessageQueue _listeningQueue;

        public void Start()
        {
            if (!MessageQueue.Exists(QueueName))
            {
                MessageQueue.Create(QueueName);
            }

            try
            {
                if (MessageQueue.Exists(QueueName))
                {
                    _listeningQueue = new MessageQueue(QueueName);
                    _listeningQueue.ReceiveCompleted += (sender, args) =>
                                                            {
                                                                args.Message.Formatter = new XmlMessageFormatter(new[] { typeof(string) });
                                                                var msg = args.Message.Body.ToString();

                                                                Console.WriteLine("Message received:{0}", msg);

                                                                var parts = msg.Split('|');
                                                                var json = parts[0];
                                                                var typeName = parts[1];
                                                                var type = Type.GetType(typeName);
                                                                var evt = (Event)JsonConvert.DeserializeObject(json, type);

                                                                new Denormaliser().StoreEvent(evt);

                                                                _listeningQueue.BeginReceive();
                                                            };
                    _listeningQueue.BeginReceive();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Unable to initialise message queue on your local machine.\r\n\r\n{0}", e));
            }
        }
    }
}