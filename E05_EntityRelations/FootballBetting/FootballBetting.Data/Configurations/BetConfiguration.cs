namespace P03_FootballBetting.Data.Configurations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class BetConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> bet)
        {
            bet.Property(b => b.DateTime)
                .HasDefaultValue(DateTime.Now);

            bet.HasOne(b => b.User)
                .WithMany(u => u.Bets)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            bet.HasOne(b => b.Game)
                .WithMany(u => u.Bets)
                .HasForeignKey(u => u.GameId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}