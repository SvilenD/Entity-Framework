namespace Cinema.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    using static Data.Configuration;

    public class ImportMovieDto
    {
        [Required]
        [StringLength(MaxLength, MinimumLength = MinLength)]
        public string Title { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        [Range(1, 10)]
        public double Rating { get; set; }

        [Required]
        [StringLength(MaxLength, MinimumLength = MinLength)]
        public string Director { get; set; }
    }
}