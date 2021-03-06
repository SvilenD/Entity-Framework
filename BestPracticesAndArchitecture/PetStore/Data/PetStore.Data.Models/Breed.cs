﻿namespace PetStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Validations.DataValidation;

    public class Breed
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        public ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();
    }
}