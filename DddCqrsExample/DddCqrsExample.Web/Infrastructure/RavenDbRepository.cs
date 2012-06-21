using DddCqrsExample.Framework;
using Raven.Client;

namespace DddCqrsExample.Web.Infrastructure
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
                return session.Load<T>(id);
            }
        }

        public void Save(T item)
        {
            using (var session = _documentStore.OpenSession())
            {
                session.Store(item);

                session.SaveChanges();
            }
        }
    }
}