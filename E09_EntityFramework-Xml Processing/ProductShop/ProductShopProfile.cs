namespace ProductShop
{
    using AutoMapper;

    using ProductShop.Models;
    using ProductShop.Dtos.Import;
    using ProductShop.Dtos.Export;
    using System.Linq;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<UserImportDto, User>();

            this.CreateMap<ProductImportDto, Product>();

            this.CreateMap<CategoryImportDto, Category>();

            this.CreateMap<CategoryProductImportDto, CategoryProduct>();

            this.CreateMap<Product, ProductExportDto>();

            this.CreateMap<Category, CategoryExportDto>()
                .ForMember(dest => dest.ProductsCount, opt => opt.MapFrom(src => src.CategoryProducts.Count))
                .ForMember(dest => dest.AveragePrice, opt => opt.MapFrom(src => src.CategoryProducts
                       .Average(p => p.Product.Price)))
                .ForMember(dest => dest.TotalRevenue, opt => opt.MapFrom(src => src.CategoryProducts
                            .Sum(p => p.Product.Price)));
        }
    }
}