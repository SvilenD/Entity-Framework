namespace Cinema.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.Configuration;

    public class Hall
    {
        public int Id { get; set; }

        [Required]
        [StringLength(MaxLength, MinimumLength = MinLength)]
        public string Name { get; set; }

        public bool Is4Dx { get; set; }

        public bool Is3D { get; set; }

        public ICollection<Projection> Projections { get; set; } = new HashSet<Projection>();

        public ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();
    }
}