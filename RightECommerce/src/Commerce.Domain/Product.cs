using System;

namespace Ploeh.Samples.Commerce.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public bool IsFeatured { get; set; }

        public DiscountedProduct ApplyDiscountFor(IUserContext userContext)
        {
            decimal discount = CalculateDiscount(userContext);

            return new DiscountedProduct(this.Name, this.UnitPrice * discount);
        }

        private static decimal CalculateDiscount(IUserContext userContext)
        {
            return userContext.IsInRole(Role.PreferredCustomer) ? .95m : 1;
        }
    }
}