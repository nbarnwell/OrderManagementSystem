using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DddCqrsEsExample.Framework
{
    public abstract class Aggregate
    {
        private readonly IList<Event> _uncommittedEvents = new List<Event>();

        public string Id { get; set; }
        public int Version { get; set; }

        public IEnumerable<Event> GetUncommittedEvents()
        {
            return new List<Event>(_uncommittedEvents);
        }

        public void AcceptUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }
        
        public void ApplyAll(IEnumerable<Event> events)
        {
            foreach (var evt in events)
            {
                InvokeEventMethod(evt);
            }
        }

        protected void Record(Event evt)
        {
            _uncommittedEvents.Add(evt);

            InvokeEventMethod(evt);
        }

        private void InvokeEventMethod(Event evt)
        {
            var args = new object[] { evt };

            var methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(m => m.Name == "Apply")
                .Where(m =>
                           {
                               ParameterInfo[] parameters = m.GetParameters();
                               return parameters.Length == 1 && parameters[0].ParameterType == evt.GetType();
                           });

            var method = methods.FirstOrDefault();

            if (method != null)
            {
                method.Invoke(this, args);
            }
        }
    }
}