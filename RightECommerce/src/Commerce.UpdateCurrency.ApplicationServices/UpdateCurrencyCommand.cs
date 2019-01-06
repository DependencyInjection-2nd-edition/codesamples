using System;
using Ploeh.Samples.Commerce.Domain;

namespace Commerce.UpdateCurrency.ApplicationServices
{
    public class UpdateCurrencyCommand : ICommand
    {
        private readonly IExchangeRateProvider provider;
        private readonly Currency currency;
        private readonly decimal rate;

        public UpdateCurrencyCommand(IExchangeRateProvider provider, Currency currency, decimal rate)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (currency == null) throw new ArgumentNullException(nameof(currency));

            this.provider = provider;
            this.currency = currency;
            this.rate = rate;
        }

        public void Execute()
        {
            decimal currentRate = this.GetCurrentRate(this.currency);

            Console.WriteLine($"Old: {currentRate} {this.currency} = 1 {Currency.Dollar}.");

            this.provider.UpdateExchangeRate(this.currency, this.rate);

            Console.WriteLine($"Updated: {this.rate} {this.currency} = 1 {Currency.Dollar}.");
        }

        private decimal GetCurrentRate(Currency currency)
        {
            var dollarConversionRates = this.provider.GetExchangeRatesFor(Currency.Dollar);
            return dollarConversionRates[currency];
        }
    }
    
    public class UpdateCurrencyArguments
    {
        public readonly Currency Currency;
        public readonly decimal Rate;

        public UpdateCurrencyArguments(Currency currency, decimal rate)
        {
            this.Currency = currency;
            this.Rate = rate;
        }
    }
}