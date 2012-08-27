using System;

namespace DddCqrsExample.Framework
{
    public abstract class Event : Message
    {
        protected Event(string aggregateId, DateTimeOffset date)
        {
            AggregateId = aggregateId;
            Date = date;
        }
        
        public string AggregateId { get; private set; }
        public DateTimeOffset Date { get; private set; }
    }
}