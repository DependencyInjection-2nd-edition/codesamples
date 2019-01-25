using System;
using Xunit;

namespace Ploeh.Samples.Commerce.Domain.Tests.Unit
{
    public class DiscountedProductTests
    {
        // Tests missing? Send us a pull request.

        [Fact]
        public void CreateWithNullNameWillThrow()
        {
            // Act
            Action action = () => CreateDiscountedProduct(name: null);

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void NameWillMatchConstructorArgument()
        {
            // Arrange
            string expectedName = "name";

            // Act
            var product = CreateDiscountedProduct(name: expectedName);

            // Assert
            Assert.Equal(expected: expectedName, actual: product.Name);
        }

        [Fact]
        public void UnitPriceWillMatchConstructorArgument()
        {
            // Arrange
            decimal expectedUnitPrice = 2.5m;

            // Act
            var product = CreateDiscountedProduct(unitPrice: expectedUnitPrice);

            // Assert
            Assert.Equal(expected: expectedUnitPrice, actual: product.UnitPrice);
        }

        private static DiscountedProduct CreateDiscountedProduct(
            string name = "Valid name", decimal? unitPrice = null)
        {
            return new DiscountedProduct(
                name: name,
                unitPrice: unitPrice ?? 0);
        }
    }
}