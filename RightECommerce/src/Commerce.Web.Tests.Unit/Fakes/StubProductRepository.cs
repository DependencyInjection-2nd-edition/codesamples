using System.Collections.Generic;
using System.Linq;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.Web.Tests.Unit.Fakes
{
    public class StubProductRepository : IProductRepository
    {
        public IEnumerable<Product> FeaturedProducts { get; set; } = Enumerable.Empty<Product>();
        public IEnumerable<Product> GetFeaturedProducts() => this.FeaturedProducts;
    }
}