namespace Cinema.DataProcessor.ImportDto
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    using static Data.Configuration;

    public class ImportHallDto
    {
        [Required]
        [StringLength(MaxLength, MinimumLength = MinLength)]
        public string Name { get; set; }

        public bool Is4Dx { get; set; }

        public bool Is3D { get; set; }

        [JsonProperty("Seats")]
        [Range(1, int.MaxValue)]
        public int SeatCount { get; set; }
    }
}