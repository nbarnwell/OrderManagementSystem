namespace DddExample.Domain.SalesOrders
{
    public class CustomerId
    {
        public CustomerId(int value)
        {
            Value = value;
        }

        public int Value { get; set; }

        public static bool operator ==(CustomerId left, CustomerId right)
        {
            return Equals(left, right);
        }

        #region R# equality implementation
        
        public static bool operator !=(CustomerId left, CustomerId right)
        {
            return !Equals(left, right);
        }

        public bool Equals(CustomerId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Value == Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(CustomerId)) return false;
            return Equals((CustomerId)obj);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        #endregion

    }
}