using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.SqlDataAccess
{
    public class SqlProductRepository : IProductRepository
    {
        private readonly CommerceContext context;

        public SqlProductRepository(CommerceContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            this.context = context;
        }

        public IEnumerable<Product> GetFeaturedProducts()
        {
            return this.GetFeaturedProductsInternal().ToArray();
        }

        private IEnumerable<Product> GetFeaturedProductsInternal()
        {
            return
                from product in this.context.Products
                where product.IsFeatured
                select product;
        }
    }
}