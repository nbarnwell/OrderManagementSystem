using System;
using DddCqrsExample.Framework;

namespace DddCqrsExample.Domain.Orders
{
    public class ItemsAddedToSalesOrderEvent : Event
    {
        public ItemsAddedToSalesOrderEvent(
            string orderId, 
            Sku sku, 
            uint quantity, 
            Money unitPrice, 
            DateTimeOffset date)
            : base(orderId, date)
        {
            if (orderId == null) throw new ArgumentNullException("orderId");
            if (sku == null) throw new ArgumentNullException("sku");
            if (unitPrice == null) throw new ArgumentNullException("unitPrice");

            OrderId = orderId;
            Sku = sku;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public string OrderId { get; private set; }
        public Sku Sku { get; private set; }
        public uint Quantity { get; private set; }
        public Money UnitPrice { get; private set; }

        protected override string GetMessageText()
        {
            return string.Format(
                "{0} item(s) (SKU: {1}, unit price: {2}) added to sales order {3} ", 
                Quantity, 
                Sku, 
                UnitPrice, 
                OrderId);
        }
    }
}