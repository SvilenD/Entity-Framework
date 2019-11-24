namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    public class SoldProductsCountExportDto
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlElement("products")]
        public SoldProductsExportDto Products { get; set; }
    }
}