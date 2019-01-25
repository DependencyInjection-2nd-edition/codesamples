using System.Linq;
using Ploeh.Samples.Commerce.Web.Models;
using Xunit;

namespace Ploeh.Samples.Commerce.Web.Tests.Unit.Models
{
    public class FeaturedProductsViewModelTests
    {
        // Tests missing? Send us a pull request.

        [Fact]
        public void ProductsIsNotNull()
        {
            // Act
            var sut = new FeaturedProductsViewModel(Enumerable.Empty<ProductViewModel>());

            // Assert
            Assert.NotNull(sut.Products);
        }
    }
}