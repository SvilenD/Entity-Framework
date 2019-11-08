namespace P03_FootballBetting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> player)
        {
            player
                .HasOne(p => p.Position)
                .WithMany(p => p.Players)
                .HasForeignKey(p => p.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            player
                .HasOne(p => p.Team)
                .WithMany(p => p.Players)
                .HasForeignKey(t => t.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}