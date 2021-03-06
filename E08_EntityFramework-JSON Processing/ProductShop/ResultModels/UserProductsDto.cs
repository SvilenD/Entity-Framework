﻿namespace ProductShop.ResultModels
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class UserProductsDto
    {
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "soldProducts")]
        public List<SoldProductsDto> SoldProducts { get; set; }
            = new List<SoldProductsDto>();
    }
}