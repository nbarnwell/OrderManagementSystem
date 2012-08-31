using System.Linq;
using System.Reflection;
using DddCqrsEsExample.Domain.Orders;
using DddCqrsEsExample.Framework;
using Simple.Data;

namespace DddCqrsEsExample.ThinReadLayer.Core
{
    public class Denormaliser
    {
        private static readonly dynamic _db = Database.OpenNamedConnection("ReadStore");

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
            _db.SalesOrders.Insert(Id: evt.OrderId, OrderValue: evt.MaxCustomerOrderValue.Amount, Currency: (int)evt.MaxCustomerOrderValue.Currency);
        }

        public void Store(ItemsAddedToSalesOrderEvent evt)
        {
            _db.SalesOrderLines.Insert(SalesOrderId: evt.OrderId, Sku: evt.Sku, Quantity: evt.Quantity, UnitPrice: evt.UnitPrice.Amount, Currency: (int)evt.UnitPrice.Currency);

            var monthlySalesRow = _db.MonthlySalesFigures.FindByYearAndMonthAndCurrency(evt.Date.Year, evt.Date.Month, evt.UnitPrice.Currency);
            if (monthlySalesRow != null)
            {
                monthlySalesRow.Amount += evt.UnitPrice.Amount * evt.Quantity;
                _db.MonthlySalesFigures.UpdateByYearAndMonthAndCurrency(monthlySalesRow);
            }
            else
            {
                _db.MonthlySalesFigures.Insert(Year: evt.Date.Year, Month: evt.Date.Month, Amount: evt.UnitPrice.Amount * evt.Quantity, Currency: evt.UnitPrice.Currency);
            }
        }
    }
}