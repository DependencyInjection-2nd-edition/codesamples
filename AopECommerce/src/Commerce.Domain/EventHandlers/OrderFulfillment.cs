using System;
using Ploeh.Samples.Commerce.Domain.Events;

namespace Ploeh.Samples.Commerce.Domain.EventHandlers
{
    // ---- Code Listing 6.3 ----
    public class OrderFulfillment : IEventHandler<OrderApproved>
    {
        private readonly ILocationService locationService;
        private readonly IInventoryManagement inventoryManagement;

        public OrderFulfillment(
            ILocationService locationService,
            IInventoryManagement inventoryManagement)
        {
            if (locationService == null) throw new ArgumentNullException(nameof(locationService));
            if (inventoryManagement == null) throw new ArgumentNullException(nameof(inventoryManagement));

            this.locationService = locationService;
            this.inventoryManagement = inventoryManagement;
        }

        public void Handle(OrderApproved e)
        {
            var warehouses = this.locationService.FindWarehouses();
            this.inventoryManagement.NotifyWarehouses(warehouses);
        }
    }
}