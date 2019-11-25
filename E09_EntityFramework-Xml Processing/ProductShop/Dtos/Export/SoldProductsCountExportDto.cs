namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    public class SoldProductsCountExportDto
    {
        [XmlElement("count")]
        public int Count { get; set; }
        [XmlArray("products")]
        public ProductExportDto[] Products { get; set; }
    }
}