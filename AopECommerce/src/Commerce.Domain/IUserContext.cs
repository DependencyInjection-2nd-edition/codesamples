using System;

namespace Ploeh.Samples.Commerce.Domain
{
    public interface IUserContext
    {
        Guid CurrentUserId { get; }

        bool IsInRole(Role permittedRole);
    }
}