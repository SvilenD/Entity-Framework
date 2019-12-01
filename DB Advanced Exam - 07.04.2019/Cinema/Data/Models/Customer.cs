namespace Cinema.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.Configuration;

    public class Customer
    {
        public int Id { get; set; }

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

        public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}