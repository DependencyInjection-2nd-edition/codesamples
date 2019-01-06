using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ploeh.Samples.Commerce.Domain.Commands;

namespace Ploeh.Samples.Commerce.Web.Presentation.Models
{
    public class AdjustInventoryViewModel
    {
        public IEnumerable<SelectListItem> Products { get; set; }
        public IEnumerable<SelectListItem> DecreaseOptions { get; set; }

        [Required]
        public AdjustInventory Command { get; set; }
    }
}