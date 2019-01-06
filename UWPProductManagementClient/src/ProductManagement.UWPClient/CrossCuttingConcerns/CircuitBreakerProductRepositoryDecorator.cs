using System;
using System.Collections.Generic;
using Ploeh.Samples.ProductManagement.Domain;

namespace Ploeh.Samples.ProductManagement.UWPClient.CrossCuttingConcerns
{
    public class CircuitBreakerProductRepositoryDecorator : IProductRepository
    {
        private readonly ICircuitBreaker breaker;
        private readonly IProductRepository decoratee;

        public CircuitBreakerProductRepositoryDecorator(
            ICircuitBreaker breaker, IProductRepository decoratee)
        {
            this.breaker = breaker;
            this.decoratee = decoratee;
        }

        public void Delete(Guid id) => this.breaker.Execute(() => this.decoratee.Delete(id));
        public IEnumerable<Product> GetAll() => this.breaker.Execute(() => this.decoratee.GetAll());
        public Product GetById(Guid id) => this.breaker.Execute(() => this.decoratee.GetById(id));
        public void Insert(Product product) => this.breaker.Execute(() => this.decoratee.Insert(product));
        public void Update(Product product) => this.breaker.Execute(() => this.decoratee.Update(product));
    }
}