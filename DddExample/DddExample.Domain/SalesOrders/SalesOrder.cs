using System;
using System.Collections.Generic;
using System.Linq;

namespace DddExample.Domain.SalesOrders
{
    public class SalesOrder
    {
        private readonly IList<SalesOrderLine> _lines;

        public SalesOrder()
        {
            _lines = new List<SalesOrderLine>();
        }

        public CustomerId CustomerId { get; private set; }
        public SalesOrderId Id { get; private set; }
        public SalesOrderStatus Status { get; private set; }
        public Address Address { get; private set; }
        public decimal TotalValue { get; private set; }
        public decimal MaxValue { get; private set; }
        
        public IEnumerable<SalesOrderLine> Lines
        {
            get { return _lines; }
        }

        public void Open(SalesOrderId salesOrderId, CustomerId customerId, Address address, decimal maxValue)
        {
            Id = salesOrderId;
            CustomerId = customerId;
            Status = SalesOrderStatus.Open;
            Address = address;
            MaxValue = maxValue;
        }

        public void Cancel()
        {
            switch (Status)
            {
                case SalesOrderStatus.Open:
                    Status = SalesOrderStatus.Cancelled;
                    return;
                default:
                    throw new InvalidOperationException("Cannot cancel an order that is no longer pending.");
            }
        }

        public void Place()
        {
            switch (Status)
            {
                case SalesOrderStatus.Open:
                    Status = SalesOrderStatus.Pending;
                    return;
                default:
                    throw new InvalidOperationException("Cannot place order that is not currently Open.");
            }
        }

        public void AddItems(uint quantity, ProductId productId, decimal itemValue)
        {
            decimal newTotalValue = TotalValue + (quantity * itemValue);

            if (newTotalValue > MaxValue)
            {
                throw new InvalidOperationException(string.Format("Adding {0} x product({1}) would make the total order value {2}, which exceeds the maximum order value of {3}", quantity, productId.Value, newTotalValue, MaxValue));
            }

            var line = _lines.SingleOrDefault(orderLine => orderLine.ProductId == productId);

            if (line == null)
            {
                _lines.Add(new SalesOrderLine(quantity, productId, itemValue));
            }
            else
            {
                line.AddQuantity(quantity);
            }

            TotalValue += quantity * itemValue;
        }
    }
}