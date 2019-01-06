using System.Collections.Generic;

namespace Ploeh.Samples.Commerce.Domain
{
    public interface IInventoryManagement
    {
        void NotifyWarehouses(IEnumerable<Warehouse> warehouses);
    }
}