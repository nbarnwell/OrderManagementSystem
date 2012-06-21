namespace DddCqrsExample.Domain.Orders
{
    public class SalesOrderLine
    {
        public Sku Sku { get; set; }

        public uint Quantity { get; set; }

        public Money UnitPrice { get; set; }
    }
}