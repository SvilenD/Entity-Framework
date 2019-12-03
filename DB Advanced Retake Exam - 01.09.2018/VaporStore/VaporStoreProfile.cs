namespace VaporStore
{
    using System;
    using System.Linq;
    using AutoMapper;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.ExportDtos;
    using VaporStore.DataProcessor.ImportDtos;

    public class VaporStoreProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public VaporStoreProfile()
        {
            this.CreateMap<CardImportDto, Card>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse(typeof(CardType), src.Type)));

            this.CreateMap<UserImportDto, User>();

            //this.CreateMap<Game, PurchaseGameExportDto>()
            //    .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            //    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.ToString("F2")));
        }
    }
}