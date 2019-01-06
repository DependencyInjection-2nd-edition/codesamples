using System;
using Ploeh.Samples.Commerce.Domain.Commands;

namespace Ploeh.Samples.Commerce.Domain.CommandServices
{
    public class DeleteProductService : ICommandService<DeleteProduct>
    {
        private readonly IProductRepository productRepository;

        public DeleteProductService(IProductRepository productRepository)
        {
            if (productRepository == null) throw new ArgumentNullException(nameof(productRepository));

            this.productRepository = productRepository;
        }

        public void Execute(DeleteProduct command)
        {
            this.productRepository.Delete(command.ProductId);
        }
    }
}