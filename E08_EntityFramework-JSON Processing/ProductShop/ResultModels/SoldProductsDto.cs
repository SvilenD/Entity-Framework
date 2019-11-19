namespace ProductShop.ResultModels
{
    using Newtonsoft.Json;

    public class SoldProductsDto
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "products")]
        public ProductsNamePriceDto[] Products { get; set; }
    }
}