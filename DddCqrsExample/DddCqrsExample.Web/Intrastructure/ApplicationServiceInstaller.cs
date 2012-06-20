using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DddCqrsExample.Framework;

namespace DddCqrsExample.Web.Intrastructure
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
                Classes.FromAssemblyNamed("DddCqrsExample.ApplicationServices")
                    .BasedOn(typeof(ICommandHandler<>))
                    .WithServiceFirstInterface()
                    .LifestylePerWebRequest()
                );
        }
    }
}