namespace MusicHub.DataProcessor.ExportDtos
{
    using Newtonsoft.Json;

    public class AlbumExportDto
    {
        [JsonProperty("AlbumName")]
        public string Name { get; set; }

        public string ReleaseDate { get; set; }

        public string ProducerName { get; set; }

        public AlbumSongsExportDto[] Songs { get; set; }

        [JsonProperty("AlbumPrice")]
        public string Price { get; set; }
    }
}