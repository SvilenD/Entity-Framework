namespace TeisterMask.DataProcessor.ExportDto
{
    using Newtonsoft.Json;

    public class EmployeeExportDto
    {
        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Tasks")]
        public EmployeeTasksExportDto[] Tasks { get; set; }
    }
}
