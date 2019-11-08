namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static P03_FootballBetting.Data.Models.Configurations.ValidationSettings.Team;

    public class Team
    {
        public int TeamId { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        [Column(TypeName = "CHAR(3)")]
        public string Initials { get; set; }

        public decimal Budget { get; set; }

        public int PrimaryKitColorId { get; set; }
        public Color PrimaryKitColor { get; set; }

        public int SecondaryKitColorId { get; set; }
        public Color SecondaryKitColor { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public ICollection<Player> Players { get; set; }
            = new HashSet<Player>();

        [NotMapped]
        public ICollection<Game> HomeGames { get; set; }
            = new HashSet<Game>();
        [NotMapped]
        public ICollection<Game> AwayGames { get; set; }
            = new HashSet<Game>();
    }
}