using System;

namespace Ploeh.Samples.Commerce.Domain
{
    public class DiscountedProduct
    {
        public DiscountedProduct(string name, decimal unitPrice)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            this.Name = name;
            this.UnitPrice = unitPrice;
        }

        public string Name { get; }
        public decimal UnitPrice { get; }
    }
}