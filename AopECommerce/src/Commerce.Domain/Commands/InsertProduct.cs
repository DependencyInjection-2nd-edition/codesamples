using System;
using System.ComponentModel.DataAnnotations;

namespace Ploeh.Samples.Commerce.Domain.Commands
{
    [PermittedRole(Role.Administrator)]
    public class InsertProduct
    {
        [RequiredGuid]
        public Guid ProductId { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public decimal? UnitPrice { get; set; }
        public string Description { get; set; }
    }
}