namespace Cinema.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Cinema.Data.Models.Enums;

    using static Data.Configuration;

    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(MaxLength, MinimumLength = MinLength)]
        public string Title { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        [Range(1, 10)]
        public double Rating { get; set; }

        [Required]
        [StringLength(MaxLength, MinimumLength = MinLength)]
        public string Director { get; set; }

        public ICollection<Projection> Projections { get; set; } = new HashSet<Projection>();
    }
}
