namespace PetClinic.Models.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("Procedure")]
    public class ProcedureExportDto
    {
        [XmlElement("Passport")]
        public string PassportSerialNumber { get; set; }

        public string OwnerNumber { get; set; }

        public string DateTime { get; set; }

        [XmlArray("AnimalAids")]
        public AnimalAidExportDto[] AnimalAids { get; set; }

        public decimal TotalPrice { get; set; } // TO F2??
    }
}
