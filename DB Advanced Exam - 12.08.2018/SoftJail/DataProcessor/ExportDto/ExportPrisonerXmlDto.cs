namespace SoftJail.DataProcessor.ExportDto
{
    using System.Xml.Serialization;

    [XmlType("Prisoner")]
    public class ExportPrisonerXmlDto
    {
        public int Id { get; set; }

        [XmlElement("Name")]
        public string FullName { get; set; }

        public string IncarcerationDate { get; set; }

        [XmlArray("EncryptedMessages")]
        public ExportMailDto[] MailDescriptions { get; set; }
    }
}
