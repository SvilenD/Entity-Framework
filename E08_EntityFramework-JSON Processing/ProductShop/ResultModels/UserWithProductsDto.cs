﻿namespace ProductShop.ResultModels
{
    using Newtonsoft.Json;

    public class UserWithProductsDto
    {
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "age")]
        public int? Age { get; set; }

        [JsonProperty(PropertyName = "soldProducts")]
        public SoldProductsDto SoldProducts { get; set; }
    }
}