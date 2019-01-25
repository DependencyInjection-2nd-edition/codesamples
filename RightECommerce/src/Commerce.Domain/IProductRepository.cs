using System.Collections.Generic;

namespace Ploeh.Samples.Commerce.Domain
{
    // ---- Code Listing 3.7 ----
    public interface IProductRepository
    {
        IEnumerable<Product> GetFeaturedProducts();
    }
}