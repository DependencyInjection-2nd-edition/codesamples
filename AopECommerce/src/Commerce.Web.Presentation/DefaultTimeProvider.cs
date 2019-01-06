using System;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.Web.Presentation
{
    public sealed class DefaultTimeProvider : ITimeProvider, IDisposable
    {
        public DateTime Now => DateTime.Now;

        public void Dispose()
        {
            // This class only implements dispose to show case releasing singletons inside the Composer.
        }
    }
}