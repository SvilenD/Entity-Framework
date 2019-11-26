namespace CarDealer.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("Supplier")]
    public class SupplierImportDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("isImporter")]
        public bool IsImporter { get; set; }
    }
}