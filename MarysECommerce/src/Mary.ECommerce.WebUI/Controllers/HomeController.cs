using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ploeh.Samples.Mary.ECommerce.DomainLogic;
using Ploeh.Samples.Mary.ECommerce.SqlDataAccess;

namespace Ploeh.Samples.Mary.ECommerce.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            bool isPreferredCustomer = this.User.IsInRole("PreferredCustomer");

            var service = new ProductService();
            IEnumerable<Product> products = service.GetFeaturedProducts(isPreferredCustomer);
            this.ViewData["Products"] = products;

            return this.View();
        }

        public ActionResult About()
        {
            return this.View();
        }
    }
}