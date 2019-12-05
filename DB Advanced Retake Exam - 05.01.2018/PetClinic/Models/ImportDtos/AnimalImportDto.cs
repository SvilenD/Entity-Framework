namespace PetClinic.Models.ImportDtos
{
    using System.ComponentModel.DataAnnotations;

    using static Constants.Animal;

    public class AnimalImportDto
    {

        [Required]
        [StringLength(MaxLength, MinimumLength = MinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(MaxLength, MinimumLength = MinLength)]
        public string Type { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Age { get; set; }

        [Required]
        public PassportImportDto Passport { get; set; }
    }
}