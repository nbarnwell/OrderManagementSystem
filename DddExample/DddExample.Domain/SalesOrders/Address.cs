namespace DddExample.Domain.SalesOrders
{
    public class Address
    {
        public string Line1 { get; private set; }
        public string Town { get; private set; }
        public PostalCode PostalCode { get; private set; }

        public Address(string line1, string town, PostalCode postalCode)
        {
            Line1 = line1;
            Town = town;
            PostalCode = postalCode;
        }

        public bool Equals(Address other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Line1, Line1) && Equals(other.Town, Town) && Equals(other.PostalCode, PostalCode);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Address)) return false;
            return Equals((Address)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Line1 != null ? Line1.GetHashCode() : 0);
                result = (result*397) ^ (Town != null ? Town.GetHashCode() : 0);
                result = (result*397) ^ (PostalCode != null ? PostalCode.GetHashCode() : 0);
                return result;
            }
        }

        public static bool operator ==(Address left, Address right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Address left, Address right)
        {
            return !Equals(left, right);
        }
    }
}