using System.Collections.Generic;

namespace Ploeh.Samples.Commerce.Domain
{
    public interface IProductService
    {
        IEnumerable<DiscountedProduct> GetFeaturedProducts();
    }
}