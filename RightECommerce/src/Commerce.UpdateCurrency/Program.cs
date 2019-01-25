using System;
using Commerce.UpdateCurrency.ApplicationServices;
using Microsoft.Extensions.Configuration;
using Ploeh.Samples.Commerce.SqlDataAccess;

namespace Commerce.UpdateCurrency
{
    // ---- Start code Listing 7.1 ----
    public static class Program
    {
        public static void Main(string[] args)
        {
            string connectionString =
                LoadConnectionString();

            CurrencyParser parser =
                CreateCurrencyParser(connectionString);

            ICommand command = parser.Parse(args);
            command.Execute();
        }

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

        // ---- Start code Listing 7.2 ----
        private static CurrencyParser CreateCurrencyParser(string connectionString)
        {
            var provider =
                new SqlExchangeRateProvider(
                    new CommerceContext(connectionString));

            return new CurrencyParser(provider);
        }
        // ---- End code Listing 7.2 ----
    }
}