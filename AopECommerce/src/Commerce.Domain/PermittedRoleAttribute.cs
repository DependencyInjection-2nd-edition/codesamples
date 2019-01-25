using System;

namespace Ploeh.Samples.Commerce.Domain
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    // ---- Start code Listing 10.17 ---- 
    public class PermittedRoleAttribute : Attribute
    {
        public readonly Role Role;

        public PermittedRoleAttribute(Role role)
        {
            this.Role = role;
        }
    }
    // ---- End code Listing 10.17 ---- 
}