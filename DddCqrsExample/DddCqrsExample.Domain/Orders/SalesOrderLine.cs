namespace DddCqrsExample.Domain.Orders
{
    public class SalesOrderLine
    {
        public SalesOrderLine(Sku sku, uint quantity, Money unitPrice)
        {
            Sku = sku;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public Sku Sku { get; private set; }
        public uint Quantity { get; private set; }
        public Money UnitPrice { get; private set; }
    }
}