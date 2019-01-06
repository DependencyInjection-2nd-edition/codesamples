using System;
using Ploeh.Samples.Commerce.Domain;

namespace Commerce.UpdateCurrency.ApplicationServices
{
    public class CurrencyParser
    {
        private const string HelpMessage = "Usage: UpdateCurrency <DKK | EUR | GBP> <rate>.";

        private readonly IExchangeRateProvider provider;

        public CurrencyParser(IExchangeRateProvider provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            this.provider = provider;
        }

        public ICommand Parse(string[] args)
        {
            decimal rate;

            if (args == null || args.Length != 2 || !decimal.TryParse(args[1], out rate))
            {
                return new HelpCommand(HelpMessage);
            }

            var currencyCode = args[0];

            return new UpdateCurrencyCommand(this.provider, new Currency(currencyCode), rate);
        }
    }
}