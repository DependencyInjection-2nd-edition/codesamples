namespace Ploeh.Samples.Commerce.Domain
{
    public interface ICurrencyConverter
    {
        Money Exchange(Money money, Currency targetCurrency);
    }
}