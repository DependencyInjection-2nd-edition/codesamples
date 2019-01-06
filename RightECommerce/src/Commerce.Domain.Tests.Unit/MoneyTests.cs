using Xunit;

namespace Ploeh.Samples.Commerce.Domain.Tests.Unit
{
    public class MoneyTests
    {
        public void ValueEquality_InstanceIsConsideredEqualToItself()
        {
            // Arrange
            var money1 = new Money(100.01m, new Currency("USD"));

            // Assert
            Assert.Equal(money1, money1);
        }

        public void ValueEquality_TwoInstancesWithSameCurrencyReferenceAndSameAmountAreConsideredEqual()
        {
            // Arrange
            var currency = new Currency("USD");
            var money1 = new Money(100.01m, currency);
            var money2 = new Money(100.01m, currency);

            // Assert
            Assert.Equal(money1, money2);
        }

        public void ValueEquality_TwoEqualInstancesAreConsideredEqual()
        {
            // Arrange
            var money1 = new Money(100.01m, new Currency("USD"));
            var money2 = new Money(100.01m, new Currency("USD"));

            // Assert
            Assert.Equal(money1, money2);
        }

        public void ValueEquality_InstancesWithDifferentAmountsAreNotConsideredEqual()
        {
            // Arrange
            var money1 = new Money(100.01m, new Currency("USD"));
            var money2 = new Money(100.02m, new Currency("USD"));

            // Assert
            Assert.Equal(money1, money2);
        }

        public void ValueEquality_InstancesWithDifferentCurrenciesAreNotConsideredEqual()
        {
            // Arrange
            var money1 = new Money(100.01m, new Currency("USD"));
            var money2 = new Money(100.01m, new Currency("EUR"));

            // Assert
            Assert.Equal(money1, money2);
        }
    }
}