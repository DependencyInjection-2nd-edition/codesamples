using Microsoft.AspNetCore.Http;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.Web
{
    // ---- Code  Listing 3.12 ----
    public class AspNetUserContextAdapter : IUserContext
    {
        private static readonly HttpContextAccessor Accessor = new HttpContextAccessor();

        public bool IsInRole(Role role)
        {
            return Accessor.HttpContext.User.IsInRole(role.ToString());
        }
    }
}