using System;
using DddCqrsExample.Framework;

namespace DddCqrsExample.Domain.Orders
{
    public class CreateSalesOrderCommand : Command
    {
        public string Id { get; private set; }
        public Money MaxValue { get; private set; }

        public CreateSalesOrderCommand(Money maxValue)
        {
            Id = Guid.NewGuid().ToString();
            MaxValue = maxValue;
        }

        protected override string GetMessageText()
        {
            return string.Format("CreateSalesOrderCommand ({0}) for a maximum value of {1}", Id, MaxValue);
        }
    }
}