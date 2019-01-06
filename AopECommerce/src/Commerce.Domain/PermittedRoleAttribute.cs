using System;

namespace Ploeh.Samples.Commerce.Domain
{
    public class PermittedRoleAttribute : Attribute
    {
        public readonly Role Role;

        public PermittedRoleAttribute(Role role)
        {
            this.Role = role;
        }
    }
}