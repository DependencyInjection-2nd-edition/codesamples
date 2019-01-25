using Microsoft.AspNetCore.Mvc;
using Ploeh.Samples.Mary.ECommerce.DomainLogic;

namespace Ploeh.Samples.Mary.ECommerce.WebUI.Controllers
{
    public class HomeController : Controller
    {
        // ---- Start code Listing 2.4 ----
        public ViewResult Index()
        {
            bool isPreferredCustomer =
                this.User.IsInRole("PreferredCustomer");

            var service = new ProductService();

            var products = service.GetFeaturedProducts(
                isPreferredCustomer);

            this.ViewData["Products"] = products;

            return this.View();
        }
        // ---- End code Listing 2.4 ----

        public ActionResult About()
        {
            return this.View();
        }
    }
}