namespace CarDealer.Dtos.Export
{
    using System.Xml.Serialization;

    //[XmlType("parts")]
    public class PartsArrayDto
    {
        [XmlElement("part")]
        public PartExportDto[] Parts { get; set; }
    }
}