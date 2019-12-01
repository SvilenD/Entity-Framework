namespace Cinema.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.Configuration;

    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [Range(typeof(decimal), DecimalMinValue, DecimalMaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public int ProjectionId { get; set; }

        public Projection Projection { get; set; }
    }
}