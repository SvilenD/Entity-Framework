namespace Lab.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Validations.Customer;

    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        public int Age { get; set; }

        public ICollection<CarPurchase> Purchases { get; set; }
            = new HashSet<CarPurchase>();

        public int? AddressId { get; set; }

        public Address Address { get; set; }
    }
}