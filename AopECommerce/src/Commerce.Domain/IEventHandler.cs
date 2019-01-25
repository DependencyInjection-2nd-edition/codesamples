namespace Ploeh.Samples.Commerce.Domain
{
    // ---- Code Listing 6.9 ---- 
    public interface IEventHandler<TEvent>
    {
        void Handle(TEvent e);
    }
}