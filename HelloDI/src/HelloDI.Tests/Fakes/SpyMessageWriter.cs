using Ploeh.Samples.HelloDI.Console;

namespace Ploeh.Samples.HelloDI.Tests.Fakes
{
    public class SpyMessageWriter : IMessageWriter
    {
        public string WrittenMessage { get; private set; }

        public int MessageCount { get; private set; }

        public void Write(string message)
        {
            this.WrittenMessage += message;
            this.MessageCount++;
        }
    }
}