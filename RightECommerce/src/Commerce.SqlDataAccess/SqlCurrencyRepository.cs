using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.SqlDataAccess
{
    public class SqlCurrencyRepository : ICurrencyRepository
    {
        private readonly CommerceContext context;

        public SqlCurrencyRepository(CommerceContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            this.context = context;
        }

        public IEnumerable<Currency> GetAllCurrencies()
        {
            string[] codes = this.context.ExchangeRates.Select(rate => rate.CurrencyCode).ToArray();

            return codes.Select(code => new Currency(code));
        }
    }
}