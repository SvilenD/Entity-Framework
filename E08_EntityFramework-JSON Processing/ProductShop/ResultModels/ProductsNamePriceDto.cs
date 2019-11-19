namespace ProductShop.ResultModels
{
    using Newtonsoft.Json;

    public class ProductsNamePriceDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }
    }
}