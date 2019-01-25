using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ploeh.Samples.Commerce.Domain;
using Ploeh.Samples.Commerce.Domain.Commands;
using Ploeh.Samples.Commerce.Web.Presentation.Models;

namespace Ploeh.Samples.Commerce.Web.Presentation.Controllers
{
    // ---- Code Listing 10.14 ----
    public class InventoryController : Controller
    {
        private readonly IProductRepository repository;
        private readonly ICommandService<AdjustInventory> inventoryAdjuster;

        public InventoryController(
            IProductRepository repository, ICommandService<AdjustInventory> inventoryAdjuster)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (inventoryAdjuster == null) throw new ArgumentNullException(nameof(inventoryAdjuster));

            this.repository = repository;
            this.inventoryAdjuster = inventoryAdjuster;
        }

        [Route("inventory/")]
        public ActionResult Index()
        {
            return this.View(this.Populate(new AdjustInventoryViewModel()));
        }

        [Route("inventory/adjustinventory")]
        public ActionResult AdjustInventory(AdjustInventoryViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(nameof(Index), this.Populate(viewModel));
            }

            AdjustInventory command = viewModel.Command;

            this.inventoryAdjuster.Execute(command);

            this.TempData["SuccessMessage"] = "Inventory successfully adjusted.";

            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private AdjustInventoryViewModel Populate(AdjustInventoryViewModel vm)
        {
            vm.Products =
                from product in this.repository.GetAll()
                select new SelectListItem(product.Name, product.Id.ToString());

            vm.DecreaseOptions = new[]
            {
                new SelectListItem("Yes", bool.TrueString),
                new SelectListItem("No", bool.FalseString)
            };

            return vm;
        }
    }
}