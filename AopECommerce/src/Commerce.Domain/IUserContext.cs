using System;

namespace Ploeh.Samples.Commerce.Domain
{
    // ---- Code Section 3.1.2 ---- 
    public interface IUserContext
    {
        Guid CurrentUserId { get; }

        bool IsInRole(Role role);
    }
}