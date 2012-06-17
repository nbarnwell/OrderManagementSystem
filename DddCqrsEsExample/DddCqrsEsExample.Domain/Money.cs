using System;

namespace DddCqrsEsExample.Domain
{
    public class Money
    {
        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }

        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public bool Equals(Money other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Amount == Amount && Equals(other.Currency, Currency);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Money)) return false;
            return Equals((Money)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Amount.GetHashCode() * 397) ^ Currency.GetHashCode();
            }
        }

        public static bool operator ==(Money left, Money right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Money left, Money right)
        {
            return !Equals(left, right);
        }

        public static bool operator >(Money left, Money right)
        {
            CurrencyCheck(left, right);

            return left.Amount > right.Amount;
        }

        public static bool operator <(Money left, Money right)
        {
            CurrencyCheck(left, right);

            return left.Amount < right.Amount;
        }

        public static Money operator +(Money left, Money right)
        {
            CurrencyCheck(left, right);

            return new Money(left.Amount + right.Amount, left.Currency);
        }

        public static Money operator -(Money left, Money right)
        {
            CurrencyCheck(left, right);

            return new Money(left.Amount - right.Amount, left.Currency);
        }

        public static Money operator *(Money left, Money right)
        {
            CurrencyCheck(left, right);

            return new Money(left.Amount * right.Amount, left.Currency);
        }

        public static Money operator /(Money left, Money right)
        {
            CurrencyCheck(left, right);

            return new Money(left.Amount / right.Amount, left.Currency);
        }



        /*
         * Money/int comparisons
         */
        public static Money operator +(Money left, int right)
        {
            return new Money(left.Amount + right, left.Currency);
        }

        public static Money operator -(Money left, int right)
        {
            return new Money(left.Amount - right, left.Currency);
        }

        public static Money operator *(Money left, int right)
        {
            return new Money(left.Amount * right, left.Currency);
        }

        public static Money operator /(Money left, int right)
        {
            return new Money(left.Amount / right, left.Currency);
        }

        public static Money operator +(int left, Money right)
        {
            return new Money(left + right.Amount, right.Currency);
        }

        public static Money operator -(int left, Money right)
        {
            return new Money(left - right.Amount, right.Currency);
        }

        public static Money operator *(int left, Money right)
        {
            return new Money(left * right.Amount, right.Currency);
        }

        public static Money operator /(int left, Money right)
        {
            return new Money(left / right.Amount, right.Currency);
        }

        /*
         * Money/uint comparisons
         */
        public static Money operator +(Money left, uint right)
        {
            return new Money(left.Amount + right, left.Currency);
        }

        public static Money operator -(Money left, uint right)
        {
            return new Money(left.Amount - right, left.Currency);
        }

        public static Money operator *(Money left, uint right)
        {
            return new Money(left.Amount * right, left.Currency);
        }

        public static Money operator /(Money left, uint right)
        {
            return new Money(left.Amount / right, left.Currency);
        }

        public static Money operator +(uint left, Money right)
        {
            return new Money(left + right.Amount, right.Currency);
        }

        public static Money operator -(uint left, Money right)
        {
            return new Money(left - right.Amount, right.Currency);
        }

        public static Money operator *(uint left, Money right)
        {
            return new Money(left * right.Amount, right.Currency);
        }

        public static Money operator /(uint left, Money right)
        {
            return new Money(left / right.Amount, right.Currency);
        }




        private static void CurrencyCheck(Money left, Money right)
        {
            if (left.Currency != right.Currency)
            {
                throw new ArithmeticException("Unable to perform arithmetic operations on Money values of different Currency.");
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Amount, Currency);
        }
    }
}