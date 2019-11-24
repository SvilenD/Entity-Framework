namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    public class SoldProductsExportDto
    {
        [XmlElement("Product")]
        public ProductExportDto[] Products { get; set; }
    }
}
