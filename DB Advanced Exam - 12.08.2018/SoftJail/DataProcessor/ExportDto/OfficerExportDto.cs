namespace SoftJail.DataProcessor.ExportDto
{
    using Newtonsoft.Json;

    public class OfficerExportDto
    {
        [JsonProperty("OfficerName")]
        public string FullName { get; set; }

        public string Department { get; set; }

        [JsonIgnore]
        public decimal Salary { get; set; }
    }
}