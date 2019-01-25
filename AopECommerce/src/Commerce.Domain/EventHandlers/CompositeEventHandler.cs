using System;
using System.Collections.Generic;

namespace Ploeh.Samples.Commerce.Domain.EventHandlers
{
    // ---- Code Listing 6.12 ----
    public class CompositeEventHandler<TEvent> : IEventHandler<TEvent>
    {
        private readonly IEnumerable<IEventHandler<TEvent>> handlers;

        public CompositeEventHandler(IEnumerable<IEventHandler<TEvent>> handlers)
        {
            if (handlers == null) throw new ArgumentNullException(nameof(handlers));

            this.handlers = handlers;
        }

        public void Handle(TEvent e)
        {
            foreach (var handler in this.handlers)
            {
                handler.Handle(e);
            }
        }
    }
}