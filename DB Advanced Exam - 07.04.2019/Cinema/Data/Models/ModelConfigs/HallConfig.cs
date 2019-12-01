namespace Cinema.Data.Models.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class HallConfig : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> hall)
        {
            hall.HasMany(h => h.Seats)
                .WithOne(s => s.Hall)
                .HasForeignKey(s => s.HallId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}