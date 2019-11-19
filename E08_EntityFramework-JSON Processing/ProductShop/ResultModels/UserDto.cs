namespace ProductShop.ResultModels
{
    public class UserDto
    {
        public int UsersCount { get; set; }

        public UserWithProductsDto[] Users { get; set; }
    }
}