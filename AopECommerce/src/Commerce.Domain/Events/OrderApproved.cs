using System;

namespace Ploeh.Samples.Commerce.Domain.Events
{
    // ---- Code Listing 6.8 ----
    public class OrderApproved
    {
        public readonly Guid OrderId;

        public OrderApproved(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}