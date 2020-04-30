using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Ploeh.Samples.Commerce.Domain;
using Ploeh.Samples.Commerce.Web.Models;

namespace Ploeh.Samples.Commerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            if (productService == null) throw new ArgumentNullException(nameof(productService));

            this.productService = productService;
        }

        public ViewResult Index()
        {
            IEnumerable<DiscountedProduct> featuredProducts =
                this.productService.GetFeaturedProducts();

            var vm = new FeaturedProductsViewModel(
                from product in featuredProducts
                select new ProductViewModel(product));

            return this.View(vm);
        }

        public ViewResult About()
        {
            return this.View();
        }
    }
}