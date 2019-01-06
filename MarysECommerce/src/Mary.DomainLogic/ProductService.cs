using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Samples.Mary.ECommerce.SqlDataAccess;

namespace Ploeh.Samples.Mary.ECommerce.DomainLogic
{
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
            return GetFeaturedProductsInternal(isCustomerPreferred).ToArray();
        }

        private IEnumerable<Product> GetFeaturedProductsInternal(bool isCustomerPreferred)
        {
            decimal discount = isCustomerPreferred ? .95m : 1;

            var featuredProducts =
                from product in this.dbContext.Products
                where product.IsFeatured
                select product;

            return
                from p in featuredProducts.AsEnumerable()
                select new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    IsFeatured = p.IsFeatured,
                    UnitPrice = p.UnitPrice * discount
                };
        }

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