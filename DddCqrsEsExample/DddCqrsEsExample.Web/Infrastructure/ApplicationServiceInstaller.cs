using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DddCqrsEsExample.Framework;

namespace DddCqrsEsExample.Web.Infrastructure
{
    public class ApplicationServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ICommandProcessor>()
                    .ImplementedBy<CommandProcessor>(),
                Component.For<IEventBus>()
                    .ImplementedBy<MsmqEventBus>()
                );

            container.Register(
                Classes.FromAssemblyNamed("DddCqrsEsExample.ApplicationServices")
                    .BasedOn(typeof(ICommandHandler<>))
                    .WithServiceFirstInterface()
                    .LifestylePerWebRequest()
                );
        }
    }
}