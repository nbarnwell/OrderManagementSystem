using System;
using DddCqrsExample.Framework;

namespace DddCqrsExample.Domain.Orders
{
    public class CreateSalesOrderCommand : Command
    {
        public CreateSalesOrderCommand(Money maxValue)
        {
            Id = Guid.NewGuid().ToString();
            MaxValue = maxValue;
        }

        public string Id { get; private set; }
        public Money MaxValue { get; private set; }

        protected override string GetMessageText()
        {
            return string.Format("CreateSalesOrderCommand ({0}) for a maximum value of {1}", Id, MaxValue);
        }
    }
}