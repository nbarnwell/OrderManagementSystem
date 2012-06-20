using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DddCqrsExample.Framework;
using Raven.Client;
using Raven.Client.Document;

namespace DddCqrsExample.Web.Intrastructure
{
    public class DataInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var documentStore = new DocumentStore { Url = "http://localhost:8080/", DefaultDatabase = "AggregateStore" };
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