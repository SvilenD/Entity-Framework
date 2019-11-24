namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    [XmlType("User")]
    public class UserProductsExportDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("soldProducts")]
        public SoldProductsExportDto SoldProducts { get; set; }
    }
}
