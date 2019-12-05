namespace PetClinic.App
{
    using AutoMapper;
    using PetClinic.Models;
    using PetClinic.Models.ExportDtos;
    using PetClinic.Models.ImportDtos;
    using System;
    using System.Globalization;
    using System.Linq;

    public class PetClinicProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public PetClinicProfile()
        {
            this.CreateMap<AnimalAidImportDto, AnimalAid>();

            this.CreateMap<PassportImportDto, Passport>()
                .ForMember(dest => dest.RegistrationDate, opt => opt
                      .MapFrom(src => DateTime.ParseExact(src.RegistrationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture)));
            this.CreateMap<AnimalImportDto, Animal>();

            this.CreateMap<VetImportDto, Vet>();

            this.CreateMap<AnimalAid, AnimalAidExportDto>();
            this.CreateMap<Procedure, ProcedureExportDto>()
                .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.DateTime.ToString("dd-MM-yyyy")))
                .ForMember(dest => dest.OwnerNumber, opt => opt.MapFrom(src => src.Animal.Passport.OwnerPhoneNumber))
                .ForMember(dest => dest.PassportSerialNumber, opt => opt.MapFrom(src => src.Animal.Passport.SerialNumber))
                .ForMember(dest => dest.AnimalAids, opt => opt
                    .MapFrom(src => src.ProcedureAnimalAids.Select(paa => paa.AnimalAid)))
                .ForMember(dest => dest.TotalPrice, opt=>opt.MapFrom(src=>src.ProcedureAnimalAids.Sum(paa=>paa.AnimalAid.Price)));
        }
    }
}
