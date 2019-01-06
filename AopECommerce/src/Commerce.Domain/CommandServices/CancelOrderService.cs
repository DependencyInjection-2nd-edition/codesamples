using System;
using Ploeh.Samples.Commerce.Domain.Commands;
using Ploeh.Samples.Commerce.Domain.Events;

namespace Ploeh.Samples.Commerce.Domain.CommandServices
{
    public class CancelOrderService : ICommandService<CancelOrder>
    {
        private readonly IOrderRepository repository;
        private readonly IEventHandler<OrderCancelled> handler;

        public CancelOrderService(
            IOrderRepository repository, IEventHandler<OrderCancelled> handler)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            this.repository = repository;
            this.handler = handler;
        }

        public void Execute(CancelOrder command)
        {
            Order order = this.repository.GetById(command.OrderId);

            order.Cancel();
            this.repository.Save(order);

            this.handler.Handle(new OrderCancelled(command.OrderId));
        }
    }
}