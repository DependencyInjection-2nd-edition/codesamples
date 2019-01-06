using System;

namespace Ploeh.Samples.Commerce.Domain
{
    public interface ICrmSystem
    {
        void CustomerCreated(Guid customerId);
    }
}