namespace PetClinic.Models.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using static Constants.AnimalAid;

    [XmlType("AnimalAid")]
    public class ProcedureAnimalAidImportDto
    {
        [Required]
        [StringLength(MaxLength, MinimumLength = MinLength)]
        public string Name { get; set; }
    }
}
