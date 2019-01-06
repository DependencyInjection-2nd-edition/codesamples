using System;

namespace Ploeh.Samples.Commerce.Domain.Commands
{
    [PermittedRole(Role.Administrator)]
    public class UpdateHasTierPricesProperty
    {
        [RequiredGuid]
        public Guid ProductId { get; set; }
        public bool HasTierPrices { get; set; }
    }
}