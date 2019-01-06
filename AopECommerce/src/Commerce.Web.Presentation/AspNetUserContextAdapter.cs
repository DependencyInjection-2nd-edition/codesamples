using System;
using Microsoft.AspNetCore.Http;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.Web.Presentation
{
    // TODO: This current implementation is faked. It should be read information from the HttpContext to get real user info.
    public class AspNetUserContextAdapter : IUserContext
    {
        private static readonly HttpContextAccessor Accessor = new HttpContextAccessor();

        public Guid CurrentUserId => Guid.NewGuid();

        public bool IsInRole(Role role) => true;
    }
}