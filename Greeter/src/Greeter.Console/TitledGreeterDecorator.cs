using System;

namespace Ploeh.Samples.Greeter.Console
{
    public class TitledGreeterDecorator : IGreeter
    {
        private readonly IGreeter decoratee;

        public TitledGreeterDecorator(IGreeter decoratee)
        {
            if (decoratee == null) throw new ArgumentNullException(nameof(decoratee));

            this.decoratee = decoratee;
        }

        public string Greet(string name)
        {
            string titledName = "Mr. " + name;
            return this.decoratee.Greet(titledName);
        }
    }
}