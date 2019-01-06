using System;
using Ploeh.Samples.Commerce.Domain.Events;

namespace Ploeh.Samples.Commerce.Domain.EventHandlers
{
    public class AccountingNotifier : IEventHandler<OrderCancelled>, IEventHandler<OrderApproved>
    {
        private readonly IBillingSystem billingSystem;

        public AccountingNotifier(IBillingSystem billingSystem)
        {
            if (billingSystem == null)
            {
                throw new ArgumentNullException(nameof(billingSystem));
            }

            this.billingSystem = billingSystem;
        }

        public void Handle(OrderCancelled e)
        {
            this.billingSystem.NotifyAccounting(e.OrderId, "Cancelled");
        }

        public void Handle(OrderApproved e)
        {
            this.billingSystem.NotifyAccounting(e.OrderId, "Approved");
        }
    }
}