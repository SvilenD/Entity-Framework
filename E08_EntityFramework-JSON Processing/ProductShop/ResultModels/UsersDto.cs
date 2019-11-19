namespace ProductShop.ResultModels
{
    using Newtonsoft.Json;

    public class UsersDto
    {
        [JsonProperty(PropertyName = "usersCount")]
        public int UsersCount { get; set; }

        [JsonProperty(PropertyName = "users")]
        public UserWithProductsDto[] Users { get; set; }
    }
}