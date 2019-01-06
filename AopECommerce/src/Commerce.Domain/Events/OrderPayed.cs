using System;

namespace Ploeh.Samples.Commerce.Domain.Events
{
    public class OrderPayed
    {
        public readonly Guid OrderId;

        public OrderPayed(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}