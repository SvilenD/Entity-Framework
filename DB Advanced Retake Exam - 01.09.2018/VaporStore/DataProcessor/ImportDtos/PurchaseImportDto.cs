namespace VaporStore.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Purchase")]
    public class PurchaseImportDto
    {
        [XmlAttribute("title")]
        public string GameName { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [XmlElement("Key")]
        [RegularExpression(@"^([A-Z0-9]{4})-([A-Z0-9]{4})-([A-Z0-9]{4})$")]
        public string ProductKey { get; set; }

        [Required]
        [XmlElement("Card")]
        public string CardNumber { get; set; }

        [Required]
        public string Date { get; set; }
    }
}