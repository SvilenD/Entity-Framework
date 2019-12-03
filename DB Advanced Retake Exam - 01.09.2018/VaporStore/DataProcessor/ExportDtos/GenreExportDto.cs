
namespace VaporStore.DataProcessor.ExportDtos
{
    using Newtonsoft.Json;

    public class GenreExportDto
    {
        public int Id { get; set; }

        [JsonProperty("Genre")]
        public string Name { get; set; }

        public GameExportDto[] Games { get; set; }

        public int TotalPlayers { get; set; }
    }
}