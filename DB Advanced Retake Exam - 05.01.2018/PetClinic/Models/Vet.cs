namespace PetClinic.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Constants.Vet;

    public class Vet
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = MinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ProfessionMaxLength, MinimumLength = MinLength)]
        public string Profession { get; set; }

        [Required]
        [Range(MinAge, MaxAge)]
        public int Age { get; set; }

        [Required]
        [RegularExpression(@"^((\+359)|0)(\d{9})$")]
        public string PhoneNumber { get; set; }

        public ICollection<Procedure> Procedures { get; set; } = new HashSet<Procedure>();
    }
}