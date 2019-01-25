using Ploeh.Samples.Commerce.Domain.Tests.Unit.Fakes;
using Xunit;

namespace Ploeh.Samples.Commerce.Domain.Tests.Unit
{
    public class ProductTests
    {
        // Tests missing? Send us a pull request.

        [Fact]
        public void ProductContainsWellBehavedWritableProperties()
        {
            // Arrange
            var expected = new { Name = "Anything", UnitPrice = 123.45m };

            var sut = new Product();

            // Act
            sut.Name = expected.Name;
            sut.UnitPrice = expected.UnitPrice;

            var actual = new { Name = sut.Name, UnitPrice = sut.UnitPrice };

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyDiscountWillNotApplyDiscountWhenUserIsNotPreferred()
        {
            // Arrange
            decimal productUnitPrice = 12.3m;
            decimal expectedUnitPrice = 12.3m;

            var sut = CreateProduct(unitPrice: productUnitPrice);

            // Act
            var discount = sut.ApplyDiscountFor(new StubUserContext());

            // Assert
            Assert.Equal(expected: expectedUnitPrice, actual: discount.UnitPrice);
        }

        [Fact]
        public void ApplyDiscountWillApplyDiscountWhenUserIsPreferred()
        {
            // Arrange
            var preferredCustomer = new StubUserContext(Role.PreferredCustomer);

            decimal productUnitPrice = 25m;
            decimal expectedUnitPrice = 23.75m;
            Product sut = CreateProduct(unitPrice: productUnitPrice);

            // Act
            var discount = sut.ApplyDiscountFor(preferredCustomer);

            // Assert
            Assert.Equal(expected: expectedUnitPrice, actual: discount.UnitPrice);
        }

        private static Product CreateProduct(decimal unitPrice)
        {
            return new Product { UnitPrice = unitPrice, Name = "Anything" };
        }
    }
}