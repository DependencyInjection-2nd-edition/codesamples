using System.Collections.Generic;

namespace Ploeh.Samples.Commerce.Web.Models
{
    public class FeaturedProductsViewModel
    {
        public FeaturedProductsViewModel(IEnumerable<ProductViewModel> products)
        {
            this.Products = products;
        }

        public IEnumerable<ProductViewModel> Products { get; }
    }
}