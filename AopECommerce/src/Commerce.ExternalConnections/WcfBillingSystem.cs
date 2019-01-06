using System;
using System.Diagnostics;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.ExternalConnections
{
    // Dummy implementation
    public class WcfBillingSystem : IBillingSystem
    {
        public void NotifyAccounting(Guid orderId, string notification)
        {
            // Calls the web service of the external billing system.
            Debug.WriteLine($"Accounting notified for order id {orderId} and notification '{notification}.");
        }
    }
}