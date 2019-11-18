namespace ProductShop
{
    using AutoMapper;
    using ProductShop.Models;
    using ProductShop.ResultModels;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<Product, ProductsInRangeDto>()
               .ForMember(dest => dest.Seller, opt => opt.MapFrom(src => src.Seller.FirstName + " " + src.Seller.LastName));
        }
    }
}