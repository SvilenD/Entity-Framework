namespace ProductShop
{
    using AutoMapper;
    using ProductShop.Models;
    using ProductShop.ResultModels;
    using System.Linq;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<Product, ProductsInRangeDto>()
               .ForMember(dest => dest.Seller, opt => opt.MapFrom(src => src.Seller.FirstName + " " + src.Seller.LastName));

            this.CreateMap<User, UserProductsDto>()
                .ForMember(dest => dest.SoldProducts, opt => opt.MapFrom(src => src.ProductsSold));

            this.CreateMap<Category, CategoryProductsDto>()
                .ForMember(dest => dest.ProductsCount, opt => opt.MapFrom(src => src.CategoryProducts.Count))
                .ForMember(dest => dest.AveragePrice, opt => opt.MapFrom(src => $"{src.CategoryProducts.Average(p => p.Product.Price):F2}"))
                .ForMember(dest => dest.TotalRevenue, opt => opt.MapFrom(src => $"{src.CategoryProducts.Sum(p => p.Product.Price):F2}"));

            this.CreateMap<Product, ProductsNamePriceDto>();

            this.CreateMap<User, SoldProductsDto>()
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.ProductsSold.Where(p => p.Buyer != null).Count()))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ProductsSold.Where(p => p.Buyer != null)));

            this.CreateMap<User, UserWithProductsDto>()
                .ForMember(dest => dest.SoldProducts, opt => opt.MapFrom(src => src));
        }
    }
}