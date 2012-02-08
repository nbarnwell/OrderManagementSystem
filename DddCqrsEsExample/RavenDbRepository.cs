using System.Collections.Generic;
using System.Linq;
using DddCqrsEsExample.Framework;
using Raven.Client;

namespace DddCqrsEsExample
{
    public class RavenDbRepository<T> : IRepository<T> where T : Aggregate, new()
    {
        private readonly IDocumentStore _documentStore;

        public RavenDbRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public T Get(string id)
        {
            using (var session = _documentStore.OpenSession())
            {
                var events = (from container in session.Query<EventContainer>()
                              where container.Event.AggregateId == id
                              select container.Event).ToList();

                if (events.Count == 0) return null;

                var result = new T();

                result.ApplyAll(events);

                return result;
            }
        }

        public void Save(T item)
        {
            IEnumerable<Event> events = item.GetUncommittedEvents();
            using (var session = _documentStore.OpenSession())
            {
                foreach (var evt in events)
                {
                    session.Store(new EventContainer(evt));
                }

                session.SaveChanges();
            }
        }
    }
}