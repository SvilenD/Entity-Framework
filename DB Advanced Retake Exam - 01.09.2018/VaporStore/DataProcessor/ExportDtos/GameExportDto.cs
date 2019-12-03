namespace VaporStore.DataProcessor.ExportDtos
{
    using Newtonsoft.Json;

    public class GameExportDto
    {
        public int Id { get; set; }

        [JsonProperty("Title")]
        public string Name { get; set; }

        public string Developer { get; set; }

        public string Tags { get; set; }

        [JsonProperty("Players")]
        public int PlayersCount { get; set; }
    }
}