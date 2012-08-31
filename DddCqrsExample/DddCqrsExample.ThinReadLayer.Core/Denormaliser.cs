using System.Linq;
using System.Reflection;
using DddCqrsExample.Domain.Orders;
using DddCqrsExample.Framework;
using Simple.Data;

namespace DddCqrsExample.ThinReadLayer.Core
{
    public class Denormaliser
    {
        private static readonly dynamic __db = Database.OpenNamedConnection("ReadStore");

        public void StoreEvent(Event evt)
        {
            var args = new object[] { evt };

            var methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(m => m.Name == "Store")
                .Where(m =>
                {
                    ParameterInfo[] parameters = m.GetParameters();
                    return parameters.Length == 1 && parameters[0].ParameterType == evt.GetType();
                });

            var method = methods.FirstOrDefault();

            if (method != null)
            {
                method.Invoke(this, args);
            }
        }

        public void Store(SalesOrderCreatedEvent evt)
        {
            __db.SalesOrders.Insert(
            Id: evt.OrderId, 
            OrderValue: evt.MaxCustomerOrderValue.Amount, 
            Currency: (int)evt.MaxCustomerOrderValue.Currency);
        }

        public void Store(ItemsAddedToSalesOrderEvent evt)
        {
            __db.SalesOrderLines.Insert(SalesOrderId: evt.OrderId, Sku: evt.Sku, Quantity: evt.Quantity, UnitPrice: evt.UnitPrice.Amount, Currency: (int)evt.UnitPrice.Currency);

            var monthlySalesRow = __db.MonthlySalesFigures.FindByYearAndMonthAndCurrency(evt.Date.Year, evt.Date.Month, evt.UnitPrice.Currency);
            if (monthlySalesRow != null)
            {
                monthlySalesRow.Amount += evt.UnitPrice.Amount * evt.Quantity;
                __db.MonthlySalesFigures.UpdateByYearAndMonthAndCurrency(monthlySalesRow);
            }
            else
            {
                __db.MonthlySalesFigures.Insert(Year: evt.Date.Year, Month: evt.Date.Month, Amount: evt.UnitPrice.Amount * evt.Quantity, Currency: evt.UnitPrice.Currency);
            }
        }
    }
}