namespace MusicHub.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;

    public class AlbumImportDto
    {
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Invalid data")]
        public string Name { get; set; }

        public string ReleaseDate { get; set; }
    }
}