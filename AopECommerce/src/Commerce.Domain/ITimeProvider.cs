using System;
using System.Collections.Generic;
using System.Text;

namespace Ploeh.Samples.Commerce.Domain
{
    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}