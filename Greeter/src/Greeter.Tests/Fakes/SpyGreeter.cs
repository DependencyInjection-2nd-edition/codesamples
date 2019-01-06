using Ploeh.Samples.Greeter.Console;

namespace Ploeh.Samples.Greeter.Tests.Fakes
{
    public class SpyGreeter : IGreeter
    {
        public string ReturnedGreet { get; set; }

        public string SuppliedName { get; private set; }

        public int CallCount { get; private set; }

        public string Greet(string name)
        {
            this.SuppliedName = name;
            this.CallCount++;
            return this.ReturnedGreet;
        }
    }
}