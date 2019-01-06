using System.Collections.Generic;

namespace Ploeh.Samples.Commerce.Domain
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetFeaturedProducts();
    }
}