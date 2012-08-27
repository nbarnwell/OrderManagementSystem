namespace DddCqrsExample.Domain
{
    public class Sku
    {
        public Sku(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        #region R# equality implementation

        public bool Equals(Sku other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Value, Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Sku)) return false;
            return Equals((Sku)obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(Sku left, Sku right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Sku left, Sku right)
        {
            return !Equals(left, right);
        }

        #endregion
        
        public override string ToString()
        {
            return Value;
        }
    }
}