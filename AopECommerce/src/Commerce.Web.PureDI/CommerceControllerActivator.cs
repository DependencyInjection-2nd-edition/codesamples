         using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Ploeh.Samples.Commerce.Domain;
using Ploeh.Samples.Commerce.Domain.CommandServices;
using Ploeh.Samples.Commerce.Domain.EventHandlers;
using Ploeh.Samples.Commerce.Domain.Events;
using Ploeh.Samples.Commerce.ExternalConnections;
using Ploeh.Samples.Commerce.SqlDataAccess;
using Ploeh.Samples.Commerce.SqlDataAccess.Aspects;
using Ploeh.Samples.Commerce.Web.Presentation;
using Ploeh.Samples.Commerce.Web.Presentation.ApiControllers;
using Ploeh.Samples.Commerce.Web.Presentation.Controllers;

namespace Ploeh.Samples.Commerce.Web.PureDI
{
    public class CommerceControllerActivator : IControllerActivator, IDisposable
    {
        private readonly CommerceConfiguration configuration;

        // Singletons
        private readonly IUserContext userContext;
        private readonly DefaultTimeProvider timeProvider;

        public CommerceControllerActivator(CommerceConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            this.configuration = configuration;

            // Create Singletons
            this.userContext = new AspNetUserContextAdapter();
            this.timeProvider = new DefaultTimeProvider();
        }

        void IDisposable.Dispose()
        {
            // Release Singletons
            this.timeProvider.Dispose();

            // This Dispose method will only be called on application shutdown when CommerceControllerActivator
            // is configured using the AddSingleton<TService>(Func<IServiceProvider, TService>) overload (see
            // the Startup.ConfigureServices method). Be aware that is will not be called by ASP.NET Corewhen you 
            // use the AddSingleton<TService>(TService) overload.
        }

        public object Create(ControllerContext context)
        {
            return this.Create(context, context.ActionDescriptor.ControllerTypeInfo.AsType());
        }

        public Controller Create(ControllerContext cc, Type controllerType)
        {
            // Create Scoped components
            var context = new CommerceContext(this.configuration.ConnectionString).TrackDisposable(cc);
            var productRepository = new SqlProductRepository(context);
            var orderRepository = new SqlOrderRepository(context);

            // Create Transient components
            switch (controllerType.Name)
            {
                case nameof(HomeController):
                    return new HomeController();

                case nameof(InventoryController):
                    return new InventoryController(
                        productRepository,
                        this.Decorate(
                            context,
                            new AdjustInventoryService(
                                new SqlInventoryRepository(context),
                                this.Handler<InventoryAdjusted>(context))));

                case nameof(OrderController):
                    return new OrderController(
                        orderApprover: Decorate(
                            context,
                            new ApproveOrderService(
                                orderRepository,
                                this.Handler<OrderApproved>(context))),
                        orderCancellor: Decorate(
                            context,
                            new CancelOrderService(
                                orderRepository,
                                this.Handler<OrderCancelled>(context))));

                case nameof(ProductController):
                    return new ProductController(
                        productRepository,
                        productDeleter: Decorate(context, new DeleteProductService(productRepository)),
                        productInserter: Decorate(context, new InsertProductService(productRepository)),
                        productUpdater: Decorate(context, new UpdateProductService(productRepository)));

                default:
                    throw new InvalidOperationException($"Unknown controller {controllerType}.");
            }
        }

        public void Release(ControllerContext context, object controller)
        {
            (controller as IDisposable)?.Dispose();
        }

        private ICommandService<TCommand> Decorate<TCommand>(
            CommerceContext context, ICommandService<TCommand> decoratee)
        {
            // NOTE: Ambient transactions disabled as SQLite does not support it. You can turn it on 
            // after switching to SQL Server by wrapping the TransactionCommandServiceDecorator<TCommand>
            // around the SaveChangesCommandServiceDecorator<TCommand>.
            return
                new SecureCommandServiceDecorator<TCommand>(
                    this.userContext,
                    new SaveChangesCommandServiceDecorator<TCommand>(
                        context,
                        new AuditingCommandServiceDecorator<TCommand>(
                            this.userContext,
                            this.timeProvider,
                            context,
                            decoratee))
                    );
        }

        private IEventHandler<TEvent> Handler<TEvent>(CommerceContext context)
        {
            var handlers = this.GetHandlerStream(context).OfType<IEventHandler<TEvent>>();
            return new CompositeEventHandler<TEvent>(handlers.ToArray());
        }

        private IEnumerable<object> GetHandlerStream(CommerceContext context)
        {
            yield return new CrmNotifier(
                new WcfCrmSystem());

            yield return new AccountingNotifier(
                new WcfBillingSystem());

            yield return new OrderFulfillment(
                new WcfLocationService(),
                new WcfInventoryManagement());

            yield return new RefundSender(
                new SqlOrderRepository(context), 
                new WcfBillingSystem());

            yield return new TermsAndConditionsSender(
                new SmtpMailMessageService(),
                new SqlTermsRepository(context, this.timeProvider));
        }
    }
}