namespace Ploeh.Samples.Greeter.Console
{
    // ---- Code Section 9.1.1 ----
    public class FormalGreeter : IGreeter
    {
        public string Greet(string name)
        {
            return "Hello, " + name + ".";
        }
    }
}