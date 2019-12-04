namespace SoftJail.DataProcessor.ExportDto
{
    using Newtonsoft.Json;
    using System.Linq;

    public class PrisonerExportDto
    {
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Fullname { get; set; }

        public int CellNumber { get; set; }

        public OfficerExportDto[] Officers { get; set; }

        public decimal TotalOfficerSalary => this.Officers.Sum(o => o.Salary);
    }
}