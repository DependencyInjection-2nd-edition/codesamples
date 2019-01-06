using System;

namespace Ploeh.Samples.Commerce.Domain
{
    public interface IInventoryRepository
    {
        ProductInventory GetByIdOrNull(Guid id);
        void Save(ProductInventory productInventory);
    }
}