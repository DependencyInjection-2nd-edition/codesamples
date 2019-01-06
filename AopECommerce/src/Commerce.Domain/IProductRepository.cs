using System;

namespace Ploeh.Samples.Commerce.Domain
{
    public interface IProductRepository
    {
        Product GetById(Guid id);
        void Save(Product product);
        void Delete(Guid id);
        Product[] GetAll();
    }
}