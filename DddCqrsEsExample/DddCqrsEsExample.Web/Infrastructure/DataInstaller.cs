using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DddCqrsEsExample.Framework;
using Raven.Client;
using Raven.Client.Document;

namespace DddCqrsEsExample.Web.Infrastructure
{
    public class DataInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var documentStore = new DocumentStore { Url = "http://localhost:8080/", DefaultDatabase = "EventStore" };
            documentStore.Initialize();

            container.Register(
                Component.For<IReadStoreConnectionFactory>()
                    .ImplementedBy<ReadStoreConnectionFactory>(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(RavenDbRepository<>)).LifestylePerWebRequest(),
                 Component.For<IDocumentStore>().Instance(documentStore)
                );
        }
    }
}