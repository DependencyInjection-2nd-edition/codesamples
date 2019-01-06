using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.SqlDataAccess
{
    public class CommerceContext : DbContext
    {
        private readonly string connectionString;

        public CommerceContext(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Value should not be empty.", nameof(connectionString));

            this.connectionString = connectionString;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<AuditEntry> AuditEntries { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<Order> Orders { get; set; }

        public bool IsNew<TEntity>(TEntity entity) where TEntity : class
        {
            return !this.Set<TEntity>().Local.Any(e => e == entity);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Change this to 'UseSqlServer' to use SQL Server instead.
            optionsBuilder.UseSqlite(this.connectionString);
        }
    }
}