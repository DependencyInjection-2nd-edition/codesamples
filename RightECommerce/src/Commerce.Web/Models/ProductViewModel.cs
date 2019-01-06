using System.Globalization;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.Web.Models
{
    public class ProductViewModel
    {
        private static readonly CultureInfo PriceCulture = new CultureInfo("en-US");

        public ProductViewModel(DiscountedProduct product)
        {
            this.Name = product.Name;
            this.UnitPrice = product.UnitPrice;
        }

        public string Name { get; }
        public decimal UnitPrice { get; }
        public string SummaryText => string.Format(PriceCulture, "{0} ({1:C})", this.Name, this.UnitPrice);
    }
}