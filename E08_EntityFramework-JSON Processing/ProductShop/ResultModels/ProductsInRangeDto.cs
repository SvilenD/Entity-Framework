namespace ProductShop.ResultModels
{
    using Newtonsoft.Json;

    public class ProductsInRangeDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "seller")]
        public string Seller { get; set; }
    }
}