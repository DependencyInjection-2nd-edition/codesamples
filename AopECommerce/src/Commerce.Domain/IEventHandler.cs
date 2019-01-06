namespace Ploeh.Samples.Commerce.Domain
{
    public interface IEventHandler<TEvent>
    {
        void Handle(TEvent e);
    }
}