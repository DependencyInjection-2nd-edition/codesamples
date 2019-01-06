using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Ploeh.Samples.ProductManagement.Domain;

namespace Ploeh.Samples.ProductManagement.DataAccess
{
    public class FakeProductRepository : IProductRepository
    {
        private readonly Dictionary<Guid, string> products = new Dictionary<Guid, string>();

        public FakeProductRepository()
        {
            this.AddTestProducts();
        }

        public IEnumerable<Product> GetAll() => this.products.Values.Select(Deserialize);
        public Product GetById(Guid id) => Deserialize(this.products[id]);
        public void Insert(Product product) => this.products.Add(product.Id, Serialize(product));
        public void Update(Product product) => this.products[product.Id] = Serialize(product);
        public void Delete(Guid id) => this.products.Remove(id);

        private static string Serialize(Product p) => JsonConvert.SerializeObject(p);
        private static Product Deserialize(string json) => JsonConvert.DeserializeObject<Product>(json);
        
        private void AddTestProducts()
        {
            this.Insert(new Product { Id = Guid.NewGuid(), Name = "Criollo Chocolate", UnitPrice = 34.95m });
            this.Insert(new Product { Id = Guid.NewGuid(), Name = "Maldon Sea Salt", UnitPrice = 19.50m });
            this.Insert(new Product { Id = Guid.NewGuid(), Name = "Gruyère", UnitPrice = 48.50m });
            this.Insert(new Product { Id = Guid.NewGuid(), Name = "White Asparagus", UnitPrice = 39.80m });
            this.Insert(new Product { Id = Guid.NewGuid(), Name = "Anchovies", UnitPrice = 18.75m });
        }
    }
}