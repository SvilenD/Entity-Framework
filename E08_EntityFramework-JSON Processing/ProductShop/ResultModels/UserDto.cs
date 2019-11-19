using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProductShop.ResultModels
{
    public class UserDto
    {
        [JsonProperty(PropertyName = "usersCount")]
        public int UsersCount { get; set; }

        [JsonProperty(PropertyName = "users")]
        public UserWithProductsDto[] Users { get; set; }
    }
}