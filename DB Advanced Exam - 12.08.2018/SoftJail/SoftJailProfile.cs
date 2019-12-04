namespace SoftJail
{
    using AutoMapper;
    using System;
    using System.Globalization;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ExportDto;
    using SoftJail.DataProcessor.ImportDto;
    using System.Linq;

    public class SoftJailProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public SoftJailProfile()
        {
            this.CreateMap<DepartmentImportDto, Department>();
            this.CreateMap<CellImportDto, Cell>();

            this.CreateMap<PrisonerImportDto, Prisoner>()
                .ForMember(dest => dest.IncarcerationDate, opt => opt
                    .MapFrom(src => DateTime.ParseExact(src.IncarcerationDate, @"dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.ReleaseDate, opt => opt
                    .MapFrom(src => DateTime.ParseExact(src.ReleaseDate, @"dd/MM/yyyy", CultureInfo.InvariantCulture)));
            this.CreateMap<MailImportDto, Mail>();
        }
    }
}
