namespace Cinema.DataProcessor.ExportDto
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ExportMovieDto
    {
        [JsonProperty("MovieName")]
        public string Title { get; set; }

        public string Rating { get; set; }

        [JsonProperty("TotalIncomes")]
        public string TotalIncomes { get; set; }

        public List<ExportMovieCustomerDto> Customers { get; set; } = new List<ExportMovieCustomerDto>();
    }
}