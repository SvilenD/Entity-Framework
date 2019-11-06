namespace Lab.Data.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CarPurchaseConfiguration : IEntityTypeConfiguration<CarPurchase>
    {
        public void Configure(EntityTypeBuilder<CarPurchase> car)
        {
            car.HasKey(cp => new { cp.CarId, cp.CustomerId });

            car.HasOne(p => p.Customer)
                .WithMany(c => c.Purchases)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            car.HasOne(cp => cp.Car)
                .WithMany(c => c.Owners)
                .HasForeignKey(p => p.CarId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}