namespace VaporStore.DataProcessor.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("Purchase")]
    public class PurchaseExportDto
    {
        [XmlElement("Card")]
        public string CardNumber { get; set; }

        public string Cvc { get; set; }

        public string Date { get; set; }

        public PurchaseGameExportDto Game {get; set;}
    }
}