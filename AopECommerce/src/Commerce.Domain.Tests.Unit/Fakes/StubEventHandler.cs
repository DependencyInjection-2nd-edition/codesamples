namespace Ploeh.Samples.Commerce.Domain.Tests.Unit.Fakes
{
    public class StubEventHandler<TEvent> : IEventHandler<TEvent>
    {
        public void Handle(TEvent e)
        {
        }
    }
}