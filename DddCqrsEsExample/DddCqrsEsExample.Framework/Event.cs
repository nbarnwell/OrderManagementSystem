using System;

namespace DddCqrsEsExample.Framework
{
    public abstract class Event : Message
    {
        public string AggregateId { get; private set; }
        public DateTimeOffset Date { get; private set; }

        protected Event(string aggregateId, DateTimeOffset date)
        {
            AggregateId = aggregateId;
            Date = date;
        }
    }
}