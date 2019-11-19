namespace ProductShop.ResultModels
{
    using Newtonsoft.Json;

    public class CategoryProductsDto
    {
        [JsonProperty(PropertyName = "category")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "productsCount")]
        public int ProductsCount { get; set; }

        [JsonProperty(PropertyName = "averagePrice")]
        public string AveragePrice { get; set; }

        [JsonProperty(PropertyName = "totalRevenue")]
        public string TotalRevenue { get; set; }
    }
}