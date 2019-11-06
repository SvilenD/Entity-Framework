namespace Lab.Data
{
    using Lab.Data.Models;
    using Lab.Data.Models.Configurations;
    using Microsoft.EntityFrameworkCore;

    public class CarDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<Make> Makes { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CarPurchase> Purchases { get; set; }

        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DataSettings.ConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new CarConfiguration());

            //modelBuilder.ApplyConfiguration(new CarPurchaseConfiguration());

            //modelBuilder.ApplyConfiguration(new MakeConfiguration());

            //modelBuilder.ApplyConfiguration(new ModelConfiguration());

            //modelBuilder.ApplyConfiguration(new CustomerConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}