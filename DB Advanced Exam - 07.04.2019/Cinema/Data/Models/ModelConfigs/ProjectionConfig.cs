namespace Cinema.Data.Models.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProjectionConfig : IEntityTypeConfiguration<Projection>
    {
        public void Configure(EntityTypeBuilder<Projection> projection)
        {
            projection.HasOne(p => p.Movie)
                .WithMany(m => m.Projections)
                .HasForeignKey(p => p.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            projection.HasOne(p => p.Hall)
                .WithMany(h => h.Projections)
                .HasForeignKey(p => p.HallId)
                .OnDelete(DeleteBehavior.Restrict);

            projection.HasMany(p => p.Tickets)
                .WithOne(t => t.Projection)
                .HasForeignKey(t => t.ProjectionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}