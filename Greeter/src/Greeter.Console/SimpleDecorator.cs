using System;

namespace Ploeh.Samples.Greeter.Console
{
    // ---- Code Section 9.1.1 ----
    public class SimpleDecorator : IGreeter
    {
        private readonly IGreeter decoratee;

        public SimpleDecorator(IGreeter decoratee)
        {
            if (decoratee == null) throw new ArgumentNullException(nameof(decoratee));

            this.decoratee = decoratee;
        }

        public string Greet(string name)
        {
            return this.decoratee.Greet(name);
        }
    }
}