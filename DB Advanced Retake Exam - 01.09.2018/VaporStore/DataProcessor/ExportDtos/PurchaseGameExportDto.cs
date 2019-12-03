namespace VaporStore.DataProcessor.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("Game")]
    public class PurchaseGameExportDto
    {
        [XmlAttribute("title")]
        public string Name { get; set; }

        public string Genre { get; set; }

        public decimal Price { get; set; }
    }
}