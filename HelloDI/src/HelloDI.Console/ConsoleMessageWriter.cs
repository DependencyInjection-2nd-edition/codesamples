namespace Ploeh.Samples.HelloDI.Console
{
    public class ConsoleMessageWriter : IMessageWriter
    {
        public void Write(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}