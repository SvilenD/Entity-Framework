namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Album
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Invalid Name Length")]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public decimal Price => this.Songs.Sum(p => p.Price);

        public int? ProducerId { get; set; }

        public Producer Producer { get; set; }

        public List<Song> Songs { get; set; }
            = new List<Song>();
    }
}