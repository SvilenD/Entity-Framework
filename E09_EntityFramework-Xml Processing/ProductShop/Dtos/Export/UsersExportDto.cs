namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    public class UsersExportDto
    {
        [XmlElement("count")]
        public int Count { get; set; }
        [XmlArray("users")]
        public UserAgeProductsExportDto[] Users { get; set; }
    }
}