using System;
using System.ComponentModel.DataAnnotations;

namespace Ploeh.Samples.Commerce.Domain.Commands
{
    [PermittedRole(Role.Administrator)]
    public class UpdateHasDiscountsApplied
    {
        [RequiredGuid]
        public Guid ProductId { get; set; }
        [Required, StringLength(255)]
        public string DiscountDescription { get; set; }
    }
}