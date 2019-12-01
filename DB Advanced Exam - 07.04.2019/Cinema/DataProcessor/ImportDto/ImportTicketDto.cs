namespace Cinema.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    using static Data.Configuration;

    [XmlType("Ticket")]
    public class ImportTicketDto
    {
         public int ProjectionId { get; set; }

        [Required]
        [Range(typeof(decimal), DecimalMinValue, DecimalMaxValue)]
        public decimal Price { get; set; }
    }
}