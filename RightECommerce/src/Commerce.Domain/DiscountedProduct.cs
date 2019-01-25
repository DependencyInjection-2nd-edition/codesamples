using System;

namespace Ploeh.Samples.Commerce.Domain
{
    // ---- Code Listing 3.6 ----
    public class DiscountedProduct
    {
        public DiscountedProduct(string name, decimal unitPrice)
        {
            if (name == null) throw new ArgumentNullException("name");

            this.Name = name;
            this.UnitPrice = unitPrice;
        }

        public string Name { get; }
        public decimal UnitPrice { get; }
    }
}