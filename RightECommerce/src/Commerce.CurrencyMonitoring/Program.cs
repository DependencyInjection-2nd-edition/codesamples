using System;
using System.Linq;
using System.Timers;
using Microsoft.Extensions.Configuration;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.CurrencyMonitoring
{
    // ---- Start code Listing 8.14 ----
    public static class Program
    {
        private static Composer composer;

        public static void Main(string[] args)
        {
            var money = new Money(
                currency: new Currency(code: args.FirstOrDefault() ?? "EUR"),
                amount: decimal.Parse(args.Skip(1).FirstOrDefault() ?? "1"));

            composer = new Composer(LoadConnectionString());

            DisplayRates(money);

            var timer = new Timer(interval: 60000);

            timer.Elapsed += (s, e) => DisplayRates(money);
            timer.Start();

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        private static void DisplayRates(Money money)
        {
            CurrencyRateDisplayer displayer =
                composer.CreateRateDisplayer();

            displayer.DisplayRatesFor(money);
        }
        // ---- End code Listing 8.14 ----

        // ---- Start code Listing 7.1 ----
        private static string LoadConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            return configuration.GetConnectionString(
                "CommerceConnectionString");
        }
        // ---- End code Listing 7.1 ----
    }
}
