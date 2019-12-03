namespace VaporStore.DataProcessor.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("User")]
    public class UserExportDto
    {
        [XmlAttribute("username")]
        public string Username { get; set; }

        public PurchaseExportDto[] Purchases { get; set; }

        public decimal TotalSpent { get; set; }
    }
}