using System;
using System.Collections.Generic;

namespace Ploeh.Samples.ProductManagement.Domain
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(Guid id);
        void Insert(Product product);
        void Update(Product product);
        void Delete(Guid id);
    }
}