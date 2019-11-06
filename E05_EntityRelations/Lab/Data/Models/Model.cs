namespace Lab.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Validations.Model;

    public class Model
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [MaxLength(ModificationMaxLength)]
        public string Modification { get; set; }

        [Required]
        public int Year { get; set; }

        public int MakeId { get; set; }

        public Make Make { get; set; }

        public ICollection<Car> Cars { get; set; }
            = new HashSet<Car>();
    }
}