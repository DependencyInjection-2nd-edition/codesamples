using Ploeh.Samples.HelloDI.Console;

namespace Ploeh.Samples.HelloDI.Tests.Fakes
{
    // ---- Start code Listing 1.4 ----
    public class SpyMessageWriter : IMessageWriter
    {
        public string WrittenMessage { get; private set; }

        public void Write(string message)
        {
            this.WrittenMessage += message;
            this.MessageCount++;
        }
        // ---- End code Listing 1.4 ----

        public int MessageCount { get; private set; }
    }
}