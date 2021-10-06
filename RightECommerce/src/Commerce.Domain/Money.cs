using System;
using System.Diagnostics;

namespace Ploeh.Samples.Commerce.Domain
{
    [DebuggerDisplay("{Currency.Code,nq} {Amount}")]
    // ---- Start code Listing 4.6 ----
    public class Money
    {
        public readonly decimal Amount;
        public readonly Currency Currency;

        public Money(decimal amount, Currency currency)
        {
            if (currency == null) throw new ArgumentNullException("currency");

            this.Amount = amount;
            this.Currency = currency;
        }
        // ---- End code Listing 4.6 ----

        public override bool Equals(object obj)
        {
            var other = obj as Money;

            return other == null
                ? false
                : this.Amount.Equals(other.Amount) && this.Currency.Equals(other.Currency);
        }

        public override int GetHashCode() => (this.Amount, this.Currency).GetHashCode();

        public override string ToString() => string.Format("{0} {1:f5}", this.Currency, this.Amount);
    }
}