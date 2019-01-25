using System;

namespace Ploeh.Samples.Greeter.Console
{
    public class NiceToMeetYouGreeterDecorator : IGreeter
    {
        private readonly IGreeter decoratee;

        public NiceToMeetYouGreeterDecorator(IGreeter decoratee)
        {
            if (decoratee == null) throw new ArgumentNullException(nameof(decoratee));

            this.decoratee = decoratee;
        }

        // ---- Start code Section 9.1.1 ----
        public string Greet(string name)
        {
            string greet = this.decoratee.Greet(name);
            return greet + " Nice to meet you.";
        }
        // ---- End code Section 9.1.1 ----
    }
}