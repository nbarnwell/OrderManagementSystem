using System;
using DddCqrsEsExample.Framework;

namespace DddCqrsEsExample.Domain.Orders
{
    public class SalesOrder : Aggregate
    {
        private Money _orderValue;
        private Money _maxCustomerOrderValue;

        public void Create(string id, Money maxCustomerOrderValue)
        {
            Record(new SalesOrderCreatedEvent(id, maxCustomerOrderValue, DateTimeOffset.Now));
        }

        public void Apply(SalesOrderCreatedEvent evt)
        {
            Id = evt.OrderId;
            _orderValue = new Money(0, evt.MaxCustomerOrderValue.Currency);
            _maxCustomerOrderValue = evt.MaxCustomerOrderValue;
        }

        public void AddItem(Sku sku, uint quantity, Money unitPrice)
        {
            if (sku == null) throw new ArgumentNullException("sku");
            if (unitPrice == null) throw new ArgumentNullException("unitPrice");

            if (_orderValue != null && unitPrice.Currency != _orderValue.Currency)
            {
                throw new ArgumentException(string.Format("Unable to mix currencies on an order (SalesOrder value: {0}, supplied unit price: {1}", _orderValue, unitPrice));
            }

            Money itemsValue = (quantity * unitPrice);
            if ((_orderValue + itemsValue) > _maxCustomerOrderValue)
            {
                throw new InvalidOperationException(string.Format("Adding items with value of {0} would take the current order value of {1} over the customer allowed maximum of {2}", itemsValue, _orderValue, _maxCustomerOrderValue));
            }

            Record(new ItemsAddedToSalesOrderEvent(Id, sku, quantity, unitPrice, DateTimeOffset.Now));
        }

        public void Apply(ItemsAddedToSalesOrderEvent evt)
        {
            _orderValue += evt.Quantity * evt.UnitPrice;
        }
    }
}