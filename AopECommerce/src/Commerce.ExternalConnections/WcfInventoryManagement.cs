using System.Collections.Generic;
using System.Diagnostics;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.ExternalConnections
{
    // Dummy implementation
    public class WcfInventoryManagement : IInventoryManagement
    {
        public void NotifyWarehouses(IEnumerable<Warehouse> warehouses)
        {
            // Notifies the warehouses about the new order
            Debug.WriteLine("Wharehouses notified.");
        }
    }
}