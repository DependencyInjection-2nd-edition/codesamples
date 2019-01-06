using System;
using System.Collections.Generic;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.CurrencyMonitoring
{
    public class CurrencyRateDisplayer
    {
        private readonly ICurrencyRepository repository;
        private readonly ICurrencyConverter converter;

        public CurrencyRateDisplayer(ICurrencyRepository repository, ICurrencyConverter converter)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (converter == null) throw new ArgumentNullException(nameof(converter));

            this.repository = repository;
            this.converter = converter;
        }

        public void DisplayRatesFor(Money amount)
        {
            if (amount == null) throw new ArgumentNullException(nameof(amount));

            Console.WriteLine("Exchange rates for {0} at {1}:", amount, DateTime.Now);

            IEnumerable<Currency> currencies = this.repository.GetAllCurrencies();

            foreach (Currency targetCurrency in currencies)
            {
                Money rate = this.converter.Exchange(amount, targetCurrency);

                Console.WriteLine(rate);
            }
        }
    }
}