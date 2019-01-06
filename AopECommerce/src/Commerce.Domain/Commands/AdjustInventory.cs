using System;
using System.ComponentModel.DataAnnotations;

namespace Ploeh.Samples.Commerce.Domain.Commands
{
    [PermittedRole(Role.InventoryManager)]
    public class AdjustInventory
    {
        [RequiredGuid]
        public Guid ProductId { get; set; }
        public bool Decrease { get; set; }

        [Range(minimum: 1, maximum: 10000)]
        public int Quantity { get; set; }
    }
}