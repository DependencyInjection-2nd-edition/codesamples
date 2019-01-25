using System;

namespace Ploeh.Samples.Commerce.Domain
{
    // ---- Code Listing 5.10 ---- 
    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}