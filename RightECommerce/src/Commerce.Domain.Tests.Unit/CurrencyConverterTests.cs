using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace Ploeh.Samples.Commerce.Domain.Tests.Unit
{
    public class CurrencyConverterTests
    {
        [Fact]
        public void ExchangeRateGetsConvertedCorrectly()
        {
            // Arrange
            var amountInDollar = new Money(100.00m, Currency.Dollar);
            var expectedAmountInEuro = new Money(91.00m, Currency.Euro);

            var provider = new ExchangeRateProviderStub
            {
                ExchangeRates = new Dictionary<Currency, decimal>
                {
                    { Currency.Dollar, 1.00m },
                    { Currency.Euro, 0.91m },
                    { Currency.Pound, 0.77m },
                }
            };

            CurrencyConverter converter = CreateCurrencyConverter(provider: provider);

            // Act
            Money actualAmount = converter.Exchange(amountInDollar, Currency.Euro);

            // Assert
            Assert.Equal(expected: expectedAmountInEuro, actual: actualAmount);
        }

        private static CurrencyConverter CreateCurrencyConverter(IExchangeRateProvider provider = null)
        {
            return new CurrencyConverter(provider);
        }

        private class ExchangeRateProviderStub : IExchangeRateProvider
        {
            public Dictionary<Currency, decimal> ExchangeRates;

            public ReadOnlyDictionary<Currency, decimal> GetExchangeRatesFor(Currency currency) => 
                new ReadOnlyDictionary<Currency, decimal>(this.ExchangeRates);

            public void UpdateExchangeRate(Currency currency, decimal rate) => throw new Exception();
        }
    }
}