using System;
using System.Collections.Generic;

namespace Ploeh.Samples.Commerce.Domain.Tests.Unit.Fakes
{
    public class InMemoryInventoryRepository : IInventoryRepository
    {
        private readonly Dictionary<Guid, ProductInventory> inventories =
            new Dictionary<Guid, ProductInventory>();

        public ProductInventory GetByIdOrNull(Guid id)
        {
            return this.inventories.TryGetValue(id, out ProductInventory value) ? value : null;
        }

        public void Save(ProductInventory productInventory)
        {
            this.inventories[productInventory.Id] = productInventory;
        }
    }
}