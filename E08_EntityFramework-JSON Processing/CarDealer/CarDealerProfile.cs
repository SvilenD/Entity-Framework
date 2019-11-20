namespace CarDealer
{
    using AutoMapper;
    using System.Globalization;
    using System.Linq;

    using CarDealer.DTO;
    using CarDealer.Models;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<CarWithPartsId, Car>();

            this.CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate
                                                                       .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));

            this.CreateMap<Car, CarDto>();

            this.CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Car))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Car.PartCars.Sum(p => p.Part.Price)));

            this.CreateMap<SaleDto, SaleFormattedDto>()
                .ForMember(dest=>dest.Price, opt=>opt.MapFrom(src=> $"{src.Price:F2}"))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => $"{src.Discount:F2}"));
        }
    }
}