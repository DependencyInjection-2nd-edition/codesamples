using System;

namespace Ploeh.Samples.Commerce.Domain
{
    public class Order
    {
        public Guid Id { get; set; }

        public bool Approved { get; private set; }
        public bool Cancelled { get; private set; }
        public bool Payed { get; private set; }

        public void Approve()
        {
            this.Approved = true;
        }

        internal void Cancel()
        {
            this.Cancelled = true;
        }
    }
}