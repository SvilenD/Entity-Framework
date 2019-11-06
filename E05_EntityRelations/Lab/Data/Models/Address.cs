namespace Lab.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Validations.Address;

    public class Address
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        [MaxLength(TownMaxLength)]
        public string Town { get; set; }

        public Customer Customer { get; set; }
    }
}
