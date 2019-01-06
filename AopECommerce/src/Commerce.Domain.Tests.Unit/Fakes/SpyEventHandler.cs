using System.Collections.Generic;
using Xunit;

namespace Ploeh.Samples.Commerce.Domain.Tests.Unit.Fakes
{
    public class SpyEventHandler<TEvent> : IEventHandler<TEvent>
    {
        public List<TEvent> HandledEvents { get; private set; } = new List<TEvent>();

        public TEvent HandledEvent
        {
            get
            {
                Assert.Single(this.HandledEvents);
                return this.HandledEvents[0];
            }
        }

        public void Handle(TEvent e)
        {
            Assert.NotNull(e);

            this.HandledEvents.Add(e);
        }
    }
}