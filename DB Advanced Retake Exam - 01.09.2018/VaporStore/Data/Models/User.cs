namespace VaporStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelConstants;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"^([A-Z]{1}[a-z]+) (([A-Z]{1}[a-z]+))$")]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(AgeMinValue, AgeMaxValue)]
        public int Age { get; set; }

        public ICollection<Card> Cards { get; set; } = new HashSet<Card>();
    }
}