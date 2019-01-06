using System;

namespace Ploeh.Samples.Commerce.Domain.Commands
{
    [PermittedRole(Role.OrderManager)]
    public class ApproveOrder
    {
        [RequiredGuid]
        public Guid OrderId { get; set; }
    }
}