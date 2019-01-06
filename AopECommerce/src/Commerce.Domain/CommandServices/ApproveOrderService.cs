using System;
using Ploeh.Samples.Commerce.Domain.Commands;
using Ploeh.Samples.Commerce.Domain.Events;

namespace Ploeh.Samples.Commerce.Domain.CommandServices
{
    public class ApproveOrderService : ICommandService<ApproveOrder>
    {
        private readonly IOrderRepository repository;
        private readonly IEventHandler<OrderApproved> handler;

        public ApproveOrderService(
            IOrderRepository repository, IEventHandler<OrderApproved> handler)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            this.repository = repository;
            this.handler = handler;
        }

        public void Execute(ApproveOrder command)
        {
            Order order = this.repository.GetById(command.OrderId);

            order.Approve();
            this.repository.Save(order);

            this.handler.Handle(new OrderApproved(command.OrderId));
        }
    }
}