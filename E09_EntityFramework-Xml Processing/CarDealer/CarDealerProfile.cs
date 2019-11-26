namespace CarDealer
{
    using AutoMapper;
    using CarDealer.Dtos.Export;
    using CarDealer.Dtos.Import;
    using CarDealer.Models;
    using System.Linq;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<SupplierImportDto, Supplier>();

            this.CreateMap<PartImportDto, Part>();

            this.CreateMap<CarImportDto, Car>();

            this.CreateMap<SaleImportDto, Sale>();

            this.CreateMap<Car, CarExportDto>();

            this.CreateMap<Car, CarBmwExportDto>();

            this.CreateMap<Supplier, SuppliersExportDto>()
                .ForMember(dest => dest.PartsCount, opt => opt.MapFrom(src => src.Parts.Count));

            this.CreateMap<Customer, CustomerExportDto>()
                .ForMember(dest => dest.BoughtCars, opt => opt.MapFrom(src => src.Sales.Count))
                .ForMember(dest => dest.SpentMoney, opt => opt.MapFrom(src => src.Sales
                    .Sum(s => s.Car.PartCars.Sum(p => p.Part.Price))));
        }
    }
}