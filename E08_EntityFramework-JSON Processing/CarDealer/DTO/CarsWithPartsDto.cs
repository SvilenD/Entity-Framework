using Newtonsoft.Json;

namespace CarDealer.DTO
{
    public class CarsWithPartsDto
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        [JsonProperty("parts")]
        public PartDto[] Parts { get; set; }
    }
}