using Ploeh.Samples.Commerce.Domain;
using Ploeh.Samples.Commerce.Web.Models;
using Xunit;

namespace Ploeh.Samples.Commerce.Web.Tests.Unit.Models
{
    public class ProductViewModelTests
    {
        [Fact]
        public void PropertiesWillMatchConstructorArguments()
        {
            // Arrange
            var expected = new { Name = "Anything", UnitPrice = 123.45m };
 
            var sut = new ProductViewModel(new DiscountedProduct(expected.Name, expected.UnitPrice));

            // Act
            var actual = new { Name = sut.Name, UnitPrice = sut.UnitPrice };

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SummaryTextWillBeCorrect()
        {
            // Arrange
            string productName = "MyProduct";
            decimal unitPrice = 48.50m;
            string expectedSummaryText = "MyProduct ($48.50)";

            var sut = new ProductViewModel(new DiscountedProduct(productName, unitPrice));

            // Act
            string actualSummaryText = sut.SummaryText;

            // Assert
            Assert.Equal(expected: expectedSummaryText, actual: actualSummaryText);
        }
    }
}