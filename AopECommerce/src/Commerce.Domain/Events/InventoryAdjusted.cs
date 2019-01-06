using System;

namespace Ploeh.Samples.Commerce.Domain.Events
{
    public class InventoryAdjusted
    {
        public readonly Guid ProductId;
        public readonly int QuantityAdjustment;

        public InventoryAdjusted(Guid productId, int quantityAdjustment)
        {
            this.ProductId = productId;
            this.QuantityAdjustment = quantityAdjustment;
        }
    }
}