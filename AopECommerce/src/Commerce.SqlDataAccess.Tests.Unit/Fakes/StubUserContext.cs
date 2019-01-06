using System;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.SqlDataAccess.Tests.Unit.Fakes
{
    public class StubUserContext : IUserContext
    {
        public Guid CurrentUserId { get; set; }

        public bool IsInRole(Role permittedRole) => true;
    }
}