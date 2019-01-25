namespace Ploeh.Samples.Commerce.Domain
{
    // ---- Code Listing 4.6 ----
    public interface ICurrencyConverter
    {
        Money Exchange(Money money, Currency targetCurrency);
    }
}