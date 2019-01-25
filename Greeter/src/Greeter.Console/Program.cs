namespace Ploeh.Samples.Greeter.Console
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // ---- Start code Section 9.1.1 ----
            IGreeter greeter =
                new NiceToMeetYouGreeterDecorator(
                    new TitledGreeterDecorator(
                        new FormalGreeter()));

            string greet = greeter.Greet("Samuel L. Jackson");

            System.Console.WriteLine(greet);
            // ---- End code Section 9.1.1 ----

            System.Console.ReadLine();
        }
    }
}