namespace CarDealer.DTO
{
    using Newtonsoft.Json;

    public class DetailedCarDto
    {
        [JsonProperty("car")]
        public CarDto Car { get; set; }

        [JsonProperty("parts")]
        public PartDto[] Parts { get; set; }
    }
}