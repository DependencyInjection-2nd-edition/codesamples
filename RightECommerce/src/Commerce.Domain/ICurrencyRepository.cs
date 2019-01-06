using System.Collections.Generic;

namespace Ploeh.Samples.Commerce.Domain
{
    public interface ICurrencyRepository
    {
        IEnumerable<Currency> GetAllCurrencies();
    }
}