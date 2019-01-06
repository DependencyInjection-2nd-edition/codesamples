using System;

namespace Ploeh.Samples.Commerce.Domain.Events
{
    public class OrderCancelled
    {
        public readonly Guid OrderId;

        public OrderCancelled(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}