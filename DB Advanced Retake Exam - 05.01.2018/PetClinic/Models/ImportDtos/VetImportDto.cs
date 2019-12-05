namespace PetClinic.Models.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using static Constants.Vet;

    [XmlType("Vet")]
    public class VetImportDto
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = MinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ProfessionMaxLength, MinimumLength = MinLength)]
        public string Profession { get; set; }

        [Required]
        [Range(MinAge, MaxAge)]
        public int Age { get; set; }

        [Required]
        [RegularExpression(@"^((\+359)|0)(\d{9})$")]
        public string PhoneNumber { get; set; }
    }
}