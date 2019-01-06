using System;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.ExternalConnections
{
    // Dummy implementation
    public class WcfCrmSystem : ICrmSystem
    {
        // Calls the web service of the CRM service.
        public void CustomerCreated(Guid customerId)
        {
            // Calls the web service of the external CRM system.
        }
    }
}