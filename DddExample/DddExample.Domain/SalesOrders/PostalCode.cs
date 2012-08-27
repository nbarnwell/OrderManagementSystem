namespace DddExample.Domain.SalesOrders
{
    public class PostalCode
    {
        public PostalCode(string outcode, string incode)
        {
            Outcode = outcode;
            Incode = incode;
        }

        public string Outcode { get; private set; }
        public string Incode { get; private set; }

        #region R# equality implementation

        public static bool operator ==(PostalCode left, PostalCode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PostalCode left, PostalCode right)
        {
            return !Equals(left, right);
        }

        public bool Equals(PostalCode other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Outcode, Outcode) && Equals(other.Incode, Incode);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(PostalCode)) return false;
            return Equals((PostalCode)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Outcode != null ? Outcode.GetHashCode() : 0) * 397) ^ (Incode != null ? Incode.GetHashCode() : 0);
            }
        }

        #endregion

    }
}