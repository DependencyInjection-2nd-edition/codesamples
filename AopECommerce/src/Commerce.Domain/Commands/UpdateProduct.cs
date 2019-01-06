using System;
using System.ComponentModel.DataAnnotations;

namespace Ploeh.Samples.Commerce.Domain.Commands
{
    [PermittedRole(Role.Administrator)]
    public class UpdateProduct
    {
        [RequiredGuid]
        public Guid ProductId { get; set; }
        [MinLength(1), StringLength(50)]
        public string Name { get; set; }
        public decimal? UnitPrice { get; set; }
        [MinLength(1), StringLength(50)]
        public string Description { get; set; }
    }
}