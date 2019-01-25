using System.Collections.Generic;

namespace Ploeh.Samples.Commerce.Domain
{
    // ---- Code Listing 3.5 ----
    public interface IProductService
    {
        IEnumerable<DiscountedProduct> GetFeaturedProducts();
    }
}