using System.Linq;

namespace Ploeh.Samples.Commerce.Domain.Tests.Unit.Fakes
{
    public class StubUserContext : IUserContext
    {
        private readonly Role[] roles;

        public StubUserContext(params Role[] roles)
        {
            this.roles = roles;
        }

        public bool IsInRole(Role role) => this.roles.Contains(role);
    }
}