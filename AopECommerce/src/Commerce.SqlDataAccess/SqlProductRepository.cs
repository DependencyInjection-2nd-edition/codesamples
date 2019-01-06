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

        public Product GetById(Guid id)
        {
            return this.context.Products.Find(id)
                ?? throw new KeyNotFoundException($"No Product with ID '{id}' was found.");
        }

        public void Save(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            if (this.context.IsNew(product))
            {
                this.context.Products.Add(product);
            }
        }

        public void Delete(Guid id)
        {
            this.context.Products.Remove(this.GetById(id));
        }

        public Product[] GetAll()
        {
            return this.context.Products.ToArray();
        }
    }
}