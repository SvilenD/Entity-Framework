namespace PetClinic.Models.ImportDtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Procedure")]
    public class ProcedureImportDto
    {
        [Required]
        [XmlElement("Vet")]
        [StringLength(40, MinimumLength = 3)]
        public string VetName { get; set; }

        [Required]
        [XmlElement("Animal")]
        public string AnimalSerialNumber { get; set; }

        [Required]
        public string DateTime { get; set; }

        [XmlArray("AnimalAids")]
        public HashSet<ProcedureAnimalAidImportDto> AnimalAidNames { get; set; }
    }
}