using System;
using Ploeh.Samples.Commerce.Domain.Commands;

namespace Ploeh.Samples.Commerce.Domain.CommandServices
{
    public class InsertProductService : ICommandService<InsertProduct>
    {
        private readonly IProductRepository repository;

        public InsertProductService(IProductRepository repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            this.repository = repository;
        }

        public void Execute(InsertProduct command)
        {
            this.repository.Save(new Product
            {
                Id = command.ProductId,
                Name = command.Name,
                UnitPrice = command.UnitPrice.Value,
                Description = command.Description ?? string.Empty,
            });
        }
    }
}