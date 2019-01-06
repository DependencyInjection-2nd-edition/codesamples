using System;

namespace Ploeh.Samples.Commerce.Domain.Events
{
    public class OrderApproved
    {
        public readonly Guid OrderId;

        public OrderApproved(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}