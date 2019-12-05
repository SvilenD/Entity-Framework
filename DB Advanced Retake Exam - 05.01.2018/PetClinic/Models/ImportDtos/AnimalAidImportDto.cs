namespace PetClinic.Models.ImportDtos
{
    using System.ComponentModel.DataAnnotations;

    using static Constants.AnimalAid;

    public class AnimalAidImportDto
    {
        [Required]
        [StringLength(MaxLength, MinimumLength = MinLength)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }
    }
}