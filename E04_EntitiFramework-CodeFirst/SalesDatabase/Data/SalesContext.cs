namespace P03_SalesDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_SalesDatabase.Data.Models;

    public class SalesContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionSettings.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany(s => s.Sales)
                .HasForeignKey(s => s.CustomerId);

            modelBuilder
                .Entity<Sale>()
                .HasOne(s => s.Product)
                .WithMany(s => s.Sales)
                .HasForeignKey(s => s.ProductId);

            modelBuilder
                .Entity<Sale>()
                .HasOne(s => s.Store)
                .WithMany(s => s.Sales)
                .HasForeignKey(s => s.SaleId);

            modelBuilder
                .Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(250)
                .HasDefaultValue("No description");

            modelBuilder
                .Entity<Sale>()
                .Property(s => s.Date)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}