using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ploeh.Samples.Mary.ECommerce.SqlDataAccess
{
    public class CommerceContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString =
                config.GetConnectionString(
                    "CommerceConnectionString");

            optionsBuilder.UseSqlite(connectionString);
        }
    }
}