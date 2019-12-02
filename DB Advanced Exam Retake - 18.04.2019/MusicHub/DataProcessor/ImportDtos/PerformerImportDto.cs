namespace MusicHub.DataProcessor.ImportDtos
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    [XmlType("Performer")]
    public class PerformerImportDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Invalid data")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Invalid data")]
        public string LastName { get; set; }

        [Range(18, 70)]
        public int Age { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal NetWorth { get; set; }

        [XmlArray("PerformersSongs")]
        public PerformerSongImportDto[] PerformerSongs { get; set; }
    }
}