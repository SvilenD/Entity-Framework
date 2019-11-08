namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static P03_FootballBetting.Data.Models.Configurations.ValidationSettings.Position;

    public class Position
    {
        public int PositionId { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<Player> Players { get; set; }
            = new HashSet<Player>();
    }
}