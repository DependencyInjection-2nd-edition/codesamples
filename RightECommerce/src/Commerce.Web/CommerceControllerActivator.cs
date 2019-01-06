using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Ploeh.Samples.Commerce.Domain;
using Ploeh.Samples.Commerce.SqlDataAccess;
using Ploeh.Samples.Commerce.Web.Controllers;

namespace Ploeh.Samples.Commerce.Web
{
    public class CommerceControllerActivator : IControllerActivator
    {
        private readonly CommerceConfiguration configuration;

        // Singletons
        private readonly IUserContext userContext;

        public CommerceControllerActivator(CommerceConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            this.configuration = configuration;

            // Create singletons
            this.userContext = new AspNetUserContextAdapter();
        }

        public object Create(ControllerContext context) => 
            this.Create(context.ActionDescriptor.ControllerTypeInfo.AsType());

        public void Release(ControllerContext context, object controller) => 
            (controller as IDisposable)?.Dispose();

        public Controller Create(Type type)
        {
            // Create Scoped components
            var context = new CommerceContext(this.configuration.ConnectionString);

            // Create Transient components
            switch (type.Name)
            {
                case nameof(HomeController):
                    return new HomeController(
                        new ProductService(
                            this.CreateProductRepository(context),
                            this.userContext));

                //case nameof(AccountController):
                //    return new AccountController(...);

                //case nameof(UserController):
                //    return new UserController(...)

                //case nameof(LoginController):
                //    return new LoginController(...);

                default: throw new InvalidOperationException($"Unknown controller {type}.");
            }
        }
        
        private IProductRepository CreateProductRepository(CommerceContext context) =>
            (IProductRepository)Activator.CreateInstance(this.configuration.ProductRepositoryType, context);
    }
}