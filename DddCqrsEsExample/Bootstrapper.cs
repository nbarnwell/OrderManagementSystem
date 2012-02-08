using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DddCqrsEsExample.Framework;
using Raven.Client;
using Raven.Client.Converters;
using Raven.Client.Document;

namespace DddCqrsEsExample
{
    public class Bootstrapper
    {
         public IWindsorContainer Run()
         {
             var documentStore = new DocumentStore { Url = "http://localhost:8080/", DefaultDatabase = "EventStore" };
             documentStore.Initialize();

             var container = new WindsorContainer();

             var eventBus = new MsmqEventBus();

             container.Register(
                 Component.For<IWindsorContainer>().Instance(container),
                 Component.For<IDocumentStore>().Instance(documentStore),
                 Component.For<IEventBus>().Instance(eventBus),
                 Component.For(typeof(IRepository<>)).ImplementedBy(typeof(RavenDbRepository<>)).LifeStyle.Transient,
                 Component.For<ICommandProcessor>().ImplementedBy<CommandProcessor>(),
                 AllTypes.FromAssemblyInDirectory(new AssemblyFilter(".\\")).BasedOn(typeof(ICommandHandler<>)).Configure(registration => registration.LifeStyle.Transient).WithService.AllInterfaces()
                 );

             return container;
         }
    }
}