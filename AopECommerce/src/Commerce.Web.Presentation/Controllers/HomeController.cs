using Microsoft.AspNetCore.Mvc;

namespace Ploeh.Samples.Commerce.Web.Presentation.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("home")]
        [Route("home/index")]
        public ViewResult Index()
        {
            return this.View();
        }

        [Route("home/about")]
        public ViewResult About()
        {
            return this.View();
        }
    }
}