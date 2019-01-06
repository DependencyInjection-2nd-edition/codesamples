using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Samples.Commerce.Domain.Tests.Unit.Fakes;
using Xunit;

namespace Ploeh.Samples.Commerce.Domain.Tests.Unit
{
    public class ProductServiceTests
    {
        [Fact]
        public void CreateWithNullRepositoryWillThrow()
        {
            // Arrange
            IProductRepository nullRepository = null;

            // Act
            Action action = () => new ProductService(nullRepository, new StubUserContext());

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void CreateWithNullPrincipalWillThrow()
        {
            // Arrange
            IUserContext nullUserContext = null;

            // Act
            Action action = () => new ProductService(new StubProductRepository(), userContext: nullUserContext);

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void GetFeaturedProductsWillReturnInstance()
        {
            // Arrange
            var sut = new ProductService(new StubProductRepository(), new StubUserContext());

            // Act
            var result = sut.GetFeaturedProducts();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetFeaturedProductsWillReturnCorrectProduct()
        {
            // Arrange
            var expected = new { Name = "Olives", UnitPrice = 24.5m };

            var repository = new StubProductRepository
            {
                FeaturedProducts = new[] 
                {
                    new Product { Name = expected.Name, UnitPrice = expected.UnitPrice }
                }
            };

            var sut = new ProductService(repository, new StubUserContext());

            // Act
            var products = sut.GetFeaturedProducts();
            var result = products.Single();

            // Assert
            Assert.Equal(expected, actual: new { result.Name, result.UnitPrice });
        }

        [Fact]
        public void GetFeaturedProductsWillReturnCorrectProductsForNonPreferredUser()
        {
            var user = new StubUserContext();

            var expectedProducts = new[]
            {
                new { Name = "Olives", UnitPrice = 24.5m },
                new { Name = "Mushrooms", UnitPrice = 14.2m },
            };

            var featuredProducts = new[]
            {
                new Product { Name = "Olives", UnitPrice = 24.5m },
                new Product { Name = "Mushrooms", UnitPrice = 14.2m }
            };

            // Arrange
            var repository = new StubProductRepository { FeaturedProducts = featuredProducts };

            var sut = new ProductService(repository, user);

            // Act
            var actualProducts =
                from p in sut.GetFeaturedProducts()
                select new { p.Name, p.UnitPrice };

            // Assert
            Assert.True(
                condition: expectedProducts.SequenceEqual(actualProducts),
                userMessage: string.Join(", ", actualProducts.Select(p => p.ToString())));
        }

        [Fact]
        public void GetFeaturedProductsWillReturnCorrectProductsForPreferredCustomer()
        {
            var user = new StubUserContext(Role.PreferredCustomer);

            var expectedProducts = new[]
            {
                new { Name = "Olives", UnitPrice = 95m },
                new { Name = "Mushrooms", UnitPrice = 47.5m },
            };

            var featuredProducts = new[]
            {
                new Product { Name = "Olives", UnitPrice = 100m },
                new Product { Name = "Mushrooms", UnitPrice = 50m }
            };

            // Arrange
            var repository = new StubProductRepository { FeaturedProducts = featuredProducts };

            var sut = new ProductService(repository, user);

            // Act
            var actualProducts =
                from p in sut.GetFeaturedProducts()
                select new { p.Name, p.UnitPrice };

            // Assert
            Assert.True(
                condition: expectedProducts.SequenceEqual(actualProducts),
                userMessage: string.Join(", ", actualProducts.Select(p => p.ToString())));
        }

        private class StubProductRepository : IProductRepository
        {
            public IEnumerable<Product> FeaturedProducts { get; set; } = Enumerable.Empty<Product>();
            public IEnumerable<Product> GetFeaturedProducts() => this.FeaturedProducts;
        }
    }
}