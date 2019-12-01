namespace Cinema.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

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