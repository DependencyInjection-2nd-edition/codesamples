using System;

namespace Ploeh.Samples.Commerce.Domain.Events
{
    public class CustomerCreated
    {
        public readonly Guid CustomerId;
        public readonly string MailAddress;

        public CustomerCreated(Guid customerId, string mailAddress)
        {
            if (mailAddress == null) throw new ArgumentNullException(nameof(mailAddress));

            this.CustomerId = customerId;
            this.MailAddress = mailAddress;
        }
    }
}