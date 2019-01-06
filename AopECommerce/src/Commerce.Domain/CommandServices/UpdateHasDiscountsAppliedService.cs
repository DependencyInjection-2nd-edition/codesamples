using System;
using Ploeh.Samples.Commerce.Domain.Commands;

namespace Ploeh.Samples.Commerce.Domain.CommandServices
{
    public class UpdateHasDiscountsAppliedService : ICommandService<UpdateHasDiscountsApplied>
    {
        private readonly IProductRepository repository;

        public UpdateHasDiscountsAppliedService(IProductRepository repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            this.repository = repository;
        }
        
        public void Execute(UpdateHasDiscountsApplied command)
        {
            Product product = this.repository.GetById(command.ProductId);

            product.Description = command.DiscountDescription;

            this.repository.Save(product);
        }
    }
}