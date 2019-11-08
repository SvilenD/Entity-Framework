namespace P03_FootballBetting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class PlayerStatisticConfiguration : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> ps)
        {

            ps.HasKey(ps => new { ps.PlayerId, ps.GameId });

            ps.HasOne(ps => ps.Player)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(p => p.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            ps.HasOne(ps => ps.Game)
                .WithMany(g => g.PlayerStatistics)
                .HasForeignKey(g => g.GameId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}