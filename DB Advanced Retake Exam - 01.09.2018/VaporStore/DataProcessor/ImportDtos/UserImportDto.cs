namespace VaporStore.DataProcessor.ImportDtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VaporStore.Data.Models;

    using static Data.ModelConstants;

    public class UserImportDto
    {
        [Required]
        [RegularExpression(@"^([A-Z]{1}[a-z]+) (([A-Z]{1}[a-z]+))$")]
        public string FullName { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(AgeMinValue, AgeMaxValue)]
        public int Age { get; set; }

        public ICollection<Card> Cards { get; set; } = new HashSet<Card>();
    }
}