using System;
using System.Diagnostics;

namespace Ploeh.Samples.Commerce.Domain
{
    [DebuggerDisplay("{Code,nq}")]
    public class Currency
    {
        public static readonly Currency Dollar = new Currency("USD");
        public static readonly Currency Euro = new Currency("EUR");
        public static readonly Currency Pound = new Currency("GBP");

        public readonly string Code;

        public Currency(string code)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));

            this.Code = code;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Currency;

            return other != null && this.Code.Equals(other.Code);
        }

        public override int GetHashCode() => this.Code.GetHashCode();

        public override string ToString() => this.Code;
    }
}