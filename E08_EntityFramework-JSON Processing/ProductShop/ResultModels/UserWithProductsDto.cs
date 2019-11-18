namespace ProductShop.ResultModels
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class UserWithProductsDto
    {
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "soldProducts")]
        public List<ProductsSoldDto> SoldProducts { get; set; }
            = new List<ProductsSoldDto>();
    }
}