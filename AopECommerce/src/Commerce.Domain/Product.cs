using System;

namespace Ploeh.Samples.Commerce.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsFeatured { get; set; }
        public bool HasTierPrices { get; set; }
    }
}