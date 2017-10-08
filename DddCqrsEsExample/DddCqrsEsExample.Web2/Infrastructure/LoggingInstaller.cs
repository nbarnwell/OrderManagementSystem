using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DddCqrsEsExample.Logging;
using Inforigami.Regalo.Core;

namespace DddCqrsEsExample.Web2.Infrastructure
{
    public class LoggingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ILogger>()
                         .ImplementedBy<ElasticSearchLogger>());
        }
    }
}