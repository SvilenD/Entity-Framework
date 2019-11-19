namespace ProductShop.ResultModels
{
    public class UsersDto
    {
        public int UsersCount { get; set; }

        public UserWithProductsDto[] Users { get; set; }
    }
}