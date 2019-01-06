using System;

namespace Ploeh.Samples.Commerce.Domain
{
    public interface IOrderRepository
    {
        Order GetById(Guid id);
        void Save(Order order);
    }
}