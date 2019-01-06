using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Ploeh.Samples.Commerce.Domain;
using Ploeh.Samples.Commerce.Web.Controllers;
using Ploeh.Samples.Commerce.Web.Models;
using Xunit;

namespace Ploeh.Samples.Commerce.Web.Tests.Unit.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void CreateWithNullProductServiceWillThrow()
        {
            // Act
            Action action = () => new HomeController(productService: null);

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void AboutWillReturnInstance()
        {
            // Arrange
            var sut = new HomeController(new StubProductService());

            // Act
            ViewResult result = sut.About();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IndexWillReturnViewWithCorrectModel()
        {
            // Arrange
            var sut = new HomeController(new StubProductService());

            // Act
            ViewResult result = sut.Index();

            // Assert
            Assert.IsAssignableFrom<FeaturedProductsViewModel>(result.ViewData.Model);
        }

        [Fact]
        public void IndexWillReturnViewModelWithCorrectProducts()
        {
            // Arrange
            var expectedProducts = new[]
            {
                new { Name = "A", UnitPrice = 1m },
                new { Name = "B", UnitPrice = 2m },
            };

            var service = new StubProductService
            {
                FeaturedProducts = new[]
                {
                    new DiscountedProduct(name: "A", unitPrice: 1m),
                    new DiscountedProduct(name: "B", unitPrice: 2m),
                }
            };

            var sut = new HomeController(service);

            // Act
            var viewResult = sut.Index();

            var viewModel = (FeaturedProductsViewModel)viewResult.ViewData.Model;

            var result = viewModel.Products.ToList();

            var actualProducts =
                from p in result
                select new { Name = p.Name, UnitPrice = p.UnitPrice };

            // Assert
            Assert.True(
                expectedProducts.SequenceEqual(actualProducts),
                userMessage: string.Join(", ", actualProducts.Select(p => p.ToString())));
        }

        private class StubProductService : IProductService
        {
            public IEnumerable<DiscountedProduct> FeaturedProducts { get; set; } = new DiscountedProduct[0];
            public IEnumerable<DiscountedProduct> GetFeaturedProducts() => this.FeaturedProducts;
        }
    }
}