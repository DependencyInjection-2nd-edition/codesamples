using System;

namespace Ploeh.Samples.Commerce.Domain.Commands
{
    [PermittedRole(Role.Administrator)]
    public class DeleteProduct
    {
        [RequiredGuid]
        public Guid ProductId { get; set; }
    }
}