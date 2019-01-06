namespace Ploeh.Samples.Greeter.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IGreeter greeter =
                new NiceToMeetYouGreeterDecorator(
                    new TitledGreeterDecorator(
                        new FormalGreeter()));

            string greet = greeter.Greet("Samuel L. Jackson");

            System.Console.WriteLine(greet);

            System.Console.ReadLine();
        }
    }
}