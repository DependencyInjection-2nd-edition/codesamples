using System;
using Ploeh.Samples.Commerce.Domain;
using Ploeh.Samples.Commerce.SqlDataAccess;

namespace Ploeh.Samples.Commerce.CurrencyMonitoring
{
    // ---- Code Listing 8.13 ----
    public class Composer
    {
        private readonly string connectionString;

        public Composer(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));

            this.connectionString = connectionString;
        }

        public CurrencyRateDisplayer CreateRateDisplayer()
        {
            var context =
                new CommerceContext(this.connectionString);

            return new CurrencyRateDisplayer(
                new SqlCurrencyRepository(
                    context),
                new CurrencyConverter(
                    new SqlExchangeRateProvider(
                        context)));
        }
    }
}