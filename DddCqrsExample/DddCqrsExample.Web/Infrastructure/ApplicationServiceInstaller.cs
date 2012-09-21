using System.Linq;
using System.ServiceProcess;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DddCqrsExample.ApplicationServices;
using DddCqrsExample.Framework;

namespace DddCqrsExample.Web.Infrastructure
{
    public class ApplicationServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ICommandProcessor>()
                    .ImplementedBy<CommandProcessor>()
                );

            container.Register(
                Classes.FromAssemblyContaining(typeof(CommandHandlerBase<>))
                    .BasedOn(typeof(ICommandHandler<>))
                    .WithServiceFirstInterface()
                    .LifestylePerWebRequest()
                );

            if (ServiceController.GetServices().Any(o => o.ServiceName == "MSMQ"))
            {
                container.Register(
                    Component.For<IEventBus>()
                        .ImplementedBy<MsmqEventBus>()
                    );
            }
            else
            {
                container.Register(
                    Component.For<IEventBus>()
                        .ImplementedBy<InProcessEventBus>()
                    );
            }
        }
    }
}