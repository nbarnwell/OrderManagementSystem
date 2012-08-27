namespace DddExample.Domain.SalesOrders
{
    public class SalesOrderId
    {
        public SalesOrderId(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }

        #region R# equality implementation
        
        public static bool operator ==(SalesOrderId left, SalesOrderId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SalesOrderId left, SalesOrderId right)
        {
            return !Equals(left, right);
        }

        public bool Equals(SalesOrderId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Value == Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(SalesOrderId)) return false;
            return Equals((SalesOrderId)obj);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        #endregion
    }
}