namespace DddCqrsExample.Domain.Orders
{
    public class AddItemsToSalesOrderCommand
    {
        public AddItemsToSalesOrderCommand(string id, Sku sku, uint quantity, Money unitPrice)
        {
            Id = id;
            Sku = sku;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public string Id { get; private set; }
        public Sku Sku { get; private set; }
        public uint Quantity { get; private set; }
        public Money UnitPrice { get; private set; }

        public override string ToString()
        {
            return string.Format("AddItemsToSalesOrder - Id: {0}, Sku: {1}, Quantity: {2}, UnitPrice: {3}", Id, Sku, Quantity, UnitPrice);
        }
    }
}