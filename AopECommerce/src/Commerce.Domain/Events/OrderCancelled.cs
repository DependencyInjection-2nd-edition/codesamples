using System;

namespace Ploeh.Samples.Commerce.Domain.Events
{
    // ---- Code Listing 6.8 ----
    public class OrderCancelled
    {
        public readonly Guid OrderId;

        public OrderCancelled(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}