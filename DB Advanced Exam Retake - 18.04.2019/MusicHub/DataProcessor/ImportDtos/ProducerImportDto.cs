namespace MusicHub.DataProcessor.ImportDtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProducerImportDto
    {
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid Name Length")]
        public string Name { get; set; }

        [RegularExpression("[A-Z][a-z]+ [A-Z][a-z]+")]
        public string Pseudonym { get; set; }

        [RegularExpression(@"\+359 [0-9]{3} [0-9]{3} [0-9]{3}")]
        public string PhoneNumber { get; set; }

        public HashSet<AlbumImportDto> Albums { get; set; }
            = new HashSet<AlbumImportDto>();
    }
}