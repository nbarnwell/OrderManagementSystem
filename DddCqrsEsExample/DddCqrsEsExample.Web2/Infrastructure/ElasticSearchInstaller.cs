using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DddCqrsEsExample.Logging;
using DddCqrsEsExample.Monitoring;
using Elasticsearch.Net;
using Nest;

namespace DddCqrsEsExample.Web2.Infrastructure
{
    public class ElasticSearchInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var uri = new Uri("http://localhost:9200");
            var connectionPool = new SingleNodeConnectionPool(uri);
            var connectionSettings = new ConnectionSettings(connectionPool);

            connectionSettings.MapDefaultTypeIndices(
                d =>
                {
                    d.Add(typeof(LogEntry), "logentries");
                    d.Add(typeof(ItemsAddedToSalesOrderMonitoringEntry), "orderactivity");
                });

            var es = new ElasticClient(connectionSettings);

            container.Register(
                Component.For<IElasticClient>()
                         .Instance(es));
        }
    }
}