namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static P03_FootballBetting.Data.Models.Configurations.ValidationSettings.Country;

    public class Country
    {
        public int CountryId { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<Town> Towns { get; set; }
            = new HashSet<Town>();
    }
}