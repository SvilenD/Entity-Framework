namespace CarDealer.DTO
{
    using Newtonsoft.Json;

    public class SaleDto
    {
        [JsonProperty("car")]
        public CarDto Car { get; set; }

        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        public decimal Discount { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("priceWithDiscount")]
        public string PriceWithDiscount => $"{this.Price * (1 - Discount / 100m):F2}";
    }
}