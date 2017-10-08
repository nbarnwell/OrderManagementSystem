using System;
using DddCqrsEsExample.Domain.Orders;
using DddCqrsEsExample.Framework;
using Nest;

namespace DddCqrsEsExample.Monitoring
{
    public class ItemsAddedToSalesOrderMonitoringEntry
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class ItemsAddedToSalesOrderEventHandler : IEventHandler<ItemsAddedToSalesOrderEvent>
    {
        private readonly IElasticClient _elastic;

        public ItemsAddedToSalesOrderEventHandler(IElasticClient elastic)
        {
            if (elastic == null) throw new ArgumentNullException(nameof(elastic));
            _elastic = elastic;
        }

        public void Handle(ItemsAddedToSalesOrderEvent evt)
        {
            _elastic.Index(
                new ItemsAddedToSalesOrderMonitoringEntry
                {
                    DateTime = DateTime.Now, 
                    Quantity = (int)evt.Quantity,
                    Sku = evt.Sku.Value
                });
        }
    }
}