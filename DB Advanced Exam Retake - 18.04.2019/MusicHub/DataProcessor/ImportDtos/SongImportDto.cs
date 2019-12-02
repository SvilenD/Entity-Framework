namespace MusicHub.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using MusicHub.Data.Models.Enums;

    [XmlType("Song")]
    public class SongImportDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Invalid Name Length")]
        public string Name { get; set; }

        public string Duration { get; set; }

        public string CreatedOn { get; set; }

        public string Genre { get; set; }

        public int? AlbumId { get; set; }

        [Required]
        public int WriterId { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }
    }
}