using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Samples.Mary.ECommerce.SqlDataAccess;

namespace Ploeh.Samples.Mary.ECommerce.DomainLogic
{
    // ---- Start code Listing 2.3 ----
    public class ProductService : IDisposable
    {
        private readonly CommerceContext dbContext;

        public ProductService()
        {
            this.dbContext = new CommerceContext();
        }

        public IEnumerable<Product> GetFeaturedProducts(
            bool isCustomerPreferred)
        {
            decimal discount =
                isCustomerPreferred ? .95m : 1;

            var featuredProducts =
                from product in this.dbContext.Products
                where product.IsFeatured
                select product;

            return
                from product in featuredProducts.AsEnumerable()
                select new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    IsFeatured = product.IsFeatured,
                    UnitPrice =
                        product.UnitPrice * discount
                };
        }
        // ---- End code Listing 2.3 ----

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dbContext.Dispose();
            }
        }
    }
}