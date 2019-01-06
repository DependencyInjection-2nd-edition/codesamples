using System;
using Ploeh.Samples.Commerce.Domain.Commands;

namespace Ploeh.Samples.Commerce.Domain.CommandServices
{
    public class UpdateHasTierPricesPropertyService : ICommandService<UpdateHasTierPricesProperty>
    {
        private readonly IProductRepository repository;

        public UpdateHasTierPricesPropertyService(IProductRepository repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            this.repository = repository;
        }

        public void Execute(UpdateHasTierPricesProperty command)
        {
            Product product = this.repository.GetById(command.ProductId);

            product.HasTierPrices = command.HasTierPrices;

            this.repository.Save(product);
        }
    }
}
