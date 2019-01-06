using System;

namespace Ploeh.Samples.Commerce.Domain
{
    public interface IBillingSystem
    {
        void NotifyAccounting(Guid orderId, string notification);
    }
}