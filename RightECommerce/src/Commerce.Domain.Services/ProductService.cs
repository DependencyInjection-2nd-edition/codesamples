using System;
using System.Collections.Generic;
using System.Linq;

namespace Ploeh.Samples.Commerce.Domain
{
    // ---- Code Listing 3.9 ----
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;
        private readonly IUserContext userContext;

        public ProductService(
            IProductRepository repository,
            IUserContext userContext)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");
            if (userContext == null)
                throw new ArgumentNullException("userContext");

            this.repository = repository;
            this.userContext = userContext;
        }

        public IEnumerable<DiscountedProduct> GetFeaturedProducts()
        {
            return
                from product in this.repository
                    .GetFeaturedProducts()
                select product.ApplyDiscountFor(this.userContext);
        }
    }
}