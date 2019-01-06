using System;
using Ploeh.Samples.Commerce.Domain.Commands;

namespace Ploeh.Samples.Commerce.Domain.CommandServices
{
    public class UpdateProductService : ICommandService<UpdateProduct>
    {
        private readonly IProductRepository repository;

        public UpdateProductService(IProductRepository repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            this.repository = repository;
        }

        public void Execute(UpdateProduct command)
        {
            Product product = this.repository.GetById(command.ProductId);

            product.Name = command.Name ?? product.Name;
            product.UnitPrice = command.UnitPrice ?? product.UnitPrice;
            product.Description = command.Description ?? product.Description;

            this.repository.Save(product);
        }
    }
}