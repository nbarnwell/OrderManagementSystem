using System;
using DddCqrsExample.Framework;

namespace DddCqrsExample.Domain.Orders
{
    public class SalesOrderCreatedEvent : Event
    {
        public SalesOrderCreatedEvent(string orderId, Money maxCustomerOrderValue, DateTimeOffset date)
            : base(orderId, date)
        {
            OrderId = orderId;
            MaxCustomerOrderValue = maxCustomerOrderValue;
        }

        public string OrderId { get; private set; }
        public Money MaxCustomerOrderValue { get; private set; }

        protected override string GetMessageText()
        {
            return string.Format("SalesOrder created with ID {0} for maximum value of {1}", OrderId, MaxCustomerOrderValue);
        }
    }
}