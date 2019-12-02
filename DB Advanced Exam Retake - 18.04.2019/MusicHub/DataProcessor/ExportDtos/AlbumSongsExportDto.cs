namespace MusicHub.DataProcessor.ExportDtos
{
    using Newtonsoft.Json;

    public class AlbumSongsExportDto
    {
        [JsonProperty("SongName")]
        public string Name { get; set; }

        public string Price { get; set; }

        [JsonProperty("Writer")]
        public string WriterName { get; set; }
    }
}
