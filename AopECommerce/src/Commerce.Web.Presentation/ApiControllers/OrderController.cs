using System;
using Microsoft.AspNetCore.Mvc;
using Ploeh.Samples.Commerce.Domain;
using Ploeh.Samples.Commerce.Domain.Commands;

namespace Ploeh.Samples.Commerce.Web.Presentation.ApiControllers
{
    [ValidateModel]
    public class OrderController : Controller
    {
        private readonly ICommandService<ApproveOrder> orderApprover;
        private readonly ICommandService<CancelOrder> orderCancellor;

        public OrderController(
            ICommandService<ApproveOrder> orderApprover,
            ICommandService<CancelOrder> orderCancellor)
        {
            if (orderApprover == null) throw new ArgumentNullException(nameof(orderApprover));
            if (orderCancellor == null) throw new ArgumentNullException(nameof(orderCancellor));

            this.orderApprover = orderApprover;
            this.orderCancellor = orderCancellor;
        }

        [HttpPost("/api/orders/approve")]
        public void Approve([FromBody] ApproveOrder command)
        {
            this.orderApprover.Execute(command);
        }

        [HttpPost("/api/orders/cancel")]
        public void Cancel([FromBody] CancelOrder command)
        {
            this.orderCancellor.Execute(command);
        }
    }
}