using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Ploeh.Samples.Commerce.Domain;
using Ploeh.Samples.Commerce.Domain.Commands;

namespace Ploeh.Samples.Commerce.Web.Presentation.ApiControllers
{
    [ValidateModel]
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICommandService<DeleteProduct> productDeleter;
        private readonly ICommandService<InsertProduct> productInserter;
        private readonly ICommandService<UpdateProduct> productUpdater;

        public ProductController(
            IProductRepository productRepository,
            ICommandService<DeleteProduct> productDeleter,
            ICommandService<InsertProduct> productInserter,
            ICommandService<UpdateProduct> productUpdater)
        {
            if (productRepository == null) throw new ArgumentNullException(nameof(productRepository));
            if (productDeleter == null) throw new ArgumentNullException(nameof(productDeleter));
            if (productInserter == null) throw new ArgumentNullException(nameof(productInserter));
            if (productUpdater == null) throw new ArgumentNullException(nameof(productUpdater));

            this.productRepository = productRepository;
            this.productDeleter = productDeleter;
            this.productInserter = productInserter;
            this.productUpdater = productUpdater;
        }

        [HttpGet("/api/products/")]
        public IEnumerable<object> Index()
        {
            return
                from p in this.productRepository.GetAll()
                select new { p.Id, p.Name, p.UnitPrice, p.Description, p.IsFeatured };
        }

        [HttpPost("/api/products/delete")]
        public void Delete([FromBody] DeleteProduct command)
        {
            this.productDeleter.Execute(command);
        }

        [HttpPost("/api/products/insert")]
        public void Insert([FromBody] InsertProduct command)
        {
            this.productInserter.Execute(command);
        }

        [HttpPost("/api/products/update")]
        public void Update([FromBody] UpdateProduct command)
        {
            this.productUpdater.Execute(command);
        }
    }
}