namespace DddCqrsEsExample.Framework
{
    public class EventContainer
    {
        public Event Event { get; set; }

        public EventContainer(Event evt)
        {
            Event = evt;
        }
    }
}