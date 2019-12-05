namespace PetClinic.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Constants.Passport;

    public class Passport
    {
        [Key]
        [RegularExpression(@"^[A-Za-z]{7}(\d){3}$")]
        public string SerialNumber { get; set; }

        [Required]
        public Animal Animal { get; set; }

        [Required]
        [RegularExpression(@"^((\+359)|0)(\d{9})$")]
        public string OwnerPhoneNumber { get; set; }

        [Required]
        [StringLength(MaxLength, MinimumLength = MinLength)]
        public string OwnerName { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }
    }
}