namespace DddExample.Domain.SalesOrders
{
    public class SalesOrderLine
    {
        public uint Quantity { get; private set; }
        public ProductId ProductId { get; private set; }
        public decimal ItemValue { get; private set; }

        public SalesOrderLine(uint quantity, ProductId productId, decimal itemValue)
        {
            Quantity = quantity;
            ProductId = productId;
            ItemValue = itemValue;
        }

        public bool Equals(SalesOrderLine other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Quantity == Quantity && Equals(other.ProductId, ProductId) && other.ItemValue == ItemValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(SalesOrderLine)) return false;
            return Equals((SalesOrderLine)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Quantity.GetHashCode();
                result = (result*397) ^ (ProductId != null ? ProductId.GetHashCode() : 0);
                result = (result*397) ^ ItemValue.GetHashCode();
                return result;
            }
        }

        public static bool operator ==(SalesOrderLine left, SalesOrderLine right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SalesOrderLine left, SalesOrderLine right)
        {
            return !Equals(left, right);
        }

        public void AddQuantity(uint quantity)
        {
            Quantity += quantity;
        }
    }
}