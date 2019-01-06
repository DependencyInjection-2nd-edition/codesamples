using System;
using Ploeh.Samples.Commerce.Domain.Events;

namespace Ploeh.Samples.Commerce.Domain.EventHandlers
{
    public class RefundSender : IEventHandler<OrderCancelled>, IEventHandler<OrderPayed>
    {
        private readonly IOrderRepository repository;
        private readonly IBillingSystem billingSystem;

        public RefundSender(IOrderRepository repository, IBillingSystem billingSystem)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (billingSystem == null) throw new ArgumentNullException(nameof(billingSystem));

            this.repository = repository;
            this.billingSystem = billingSystem;
        }

        public void Handle(OrderCancelled e)
        {
            Order order = this.repository.GetById(e.OrderId);

            if (order.Payed)
            {
                this.billingSystem.NotifyAccounting(e.OrderId, "RequestRefund");
            }
        }

        public void Handle(OrderPayed e)
        {
            Order order = this.repository.GetById(e.OrderId);

            if (order.Cancelled)
            {
                this.billingSystem.NotifyAccounting(e.OrderId, "RequestRefund");
            }
        }
    }
}