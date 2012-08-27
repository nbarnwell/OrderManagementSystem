using System.Collections.Generic;

namespace DddCqrsExample.Framework
{
    public abstract class Aggregate
    {
        private readonly IList<Event> _uncommittedEvents = new List<Event>();

        public string Id { get; set; }

        public IEnumerable<Event> GetUncommittedEvents()
        {
            return new List<Event>(_uncommittedEvents);
        }

        public void AcceptUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        protected void Record(Event evt)
        {
            _uncommittedEvents.Add(evt);
        }
    }
}