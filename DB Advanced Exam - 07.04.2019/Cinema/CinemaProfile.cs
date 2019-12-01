namespace Cinema
{
    using System;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    
    using Cinema.Data.Models;
    using Cinema.DataProcessor.ExportDto;
    using Cinema.DataProcessor.ImportDto;

    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            this.CreateMap<ImportMovieDto, Movie>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src =>
                    TimeSpan.ParseExact(src.Duration, @"hh\:mm\:ss", CultureInfo.InvariantCulture)));

            this.CreateMap<ImportHallDto, Hall>();

            this.CreateMap<ImportProjectionDto, Projection>()
                .ForMember(dest => dest.DateTime, opt => opt
                    .MapFrom(src => DateTime.ParseExact(src.DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)));

            this.CreateMap<ImportTicketDto, Ticket>();
            this.CreateMap<ImportCustomerDto, Customer>();
        }
    }
}
