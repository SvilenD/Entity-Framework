namespace Lab.Data.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> car)
        {
            car.HasOne(c => c.Model)
               .WithMany(m => m.Cars)
               .HasForeignKey(m => m.ModelId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            car.HasIndex(c => c.VIN)
                .IsUnique();
        }
    }
}