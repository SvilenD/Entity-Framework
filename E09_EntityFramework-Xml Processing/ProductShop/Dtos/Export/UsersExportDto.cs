namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    public class UsersExportDto
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlElement("users")]
        public UserAgeProductsExportDto[] Users { get; set; }
    }
}
