namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static P03_FootballBetting.Data.Models.Configurations.ValidationSettings.Color;

    public class Color
    {
        public int ColorId { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [NotMapped]
        public ICollection<Team> PrimaryKitTeams { get; set; }
            = new HashSet<Team>();

        [NotMapped]
        public ICollection<Team> SecondaryKitTeams { get; set; }
            = new HashSet<Team>();
    }
}