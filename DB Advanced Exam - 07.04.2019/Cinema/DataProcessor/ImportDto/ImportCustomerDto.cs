namespace Cinema.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using static Data.Configuration;

    [XmlType("Customer")]
    public class ImportCustomerDto
    {
        [Required]
        [StringLength(MaxLength, MinimumLength = MinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(MaxLength, MinimumLength = MinLength)]
        public string LastName { get; set; }

        [Range(MinAge, MaxAge)]
        public int Age { get; set; }

        [Range(typeof(decimal), DecimalMinValue, DecimalMaxValue)]
        public decimal Balance { get; set; }

        [XmlArray("Tickets")]
        public ImportTicketDto[] Tickets { get; set; } 
    }
}
