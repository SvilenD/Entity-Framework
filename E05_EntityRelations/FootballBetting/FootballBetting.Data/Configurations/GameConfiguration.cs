namespace P03_FootballBetting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> game)
        {
            game
                .HasOne(g => g.HomeTeam)
                .WithMany(t => t.HomeGames)
                .HasForeignKey(t => t.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            game
                .HasOne(g => g.AwayTeam)
                .WithMany(t => t.AwayGames)
                .HasForeignKey(t => t.GameId)
                .OnDelete(DeleteBehavior.Restrict); 

            game
                .Property(g => g.Result)
                .HasDefaultValue("0 : 0");
            game
                .Property(g => g.HomeTeamBetRate)
                .HasDefaultValue(1);
            game
                .Property(g => g.AwayTeamBetRate)
                .HasDefaultValue(1);
            game
                .Property(g => g.DrawBetRate)
                .HasDefaultValue(1);
        }
    }
}