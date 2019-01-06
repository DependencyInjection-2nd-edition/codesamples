using System;
using Ploeh.Samples.Commerce.Domain.Events;

namespace Ploeh.Samples.Commerce.Domain.EventHandlers
{
    public class CrmNotifier : IEventHandler<CustomerCreated>
    {
        private readonly ICrmSystem crmSystem;

        public CrmNotifier(ICrmSystem crmSystem)
        {
            if (crmSystem == null) throw new ArgumentNullException(nameof(crmSystem));

            this.crmSystem = crmSystem;
        }

        public void Handle(CustomerCreated e)
        {
            this.crmSystem.CustomerCreated(e.CustomerId);
        }
    }
}