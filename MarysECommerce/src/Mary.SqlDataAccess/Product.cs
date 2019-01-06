using System;

namespace Ploeh.Samples.Mary.ECommerce.SqlDataAccess
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsFeatured { get; set; }
    }
}