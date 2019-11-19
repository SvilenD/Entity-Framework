namespace ProductShop.ResultModels
{
    public class SoldProductsDto
    {
        public int Count { get; set; }

        public ProductsNamePriceDto[] Products { get; set; }
    }
}