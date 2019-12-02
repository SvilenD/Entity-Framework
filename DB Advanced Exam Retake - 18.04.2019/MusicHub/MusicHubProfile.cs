namespace MusicHub
{
    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.DataProcessor.ExportDtos;
    using MusicHub.DataProcessor.ImportDtos;
    using System;
    using System.Globalization;
    using System.Linq;

    public class MusicHubProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public MusicHubProfile()
        {
            this.CreateMap<WriterImportDto, Writer>();

            this.CreateMap<AlbumImportDto, Album>()
                .ForMember(dest => dest.ReleaseDate, opt =>
                    opt.MapFrom(src => DateTime.ParseExact(src.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)));

            this.CreateMap<ProducerImportDto, Producer>();

            this.CreateMap<SongImportDto, Song>()
                    .ForMember(dest => dest.Duration, opt => opt
                        .MapFrom(src => TimeSpan.ParseExact(src.Duration, @"hh\:mm\:ss", CultureInfo.InvariantCulture)))
                    .ForMember(dest => dest.CreatedOn, opt => opt
                        .MapFrom(src => DateTime.ParseExact(src.CreatedOn, @"dd/MM/yyyy", CultureInfo.InvariantCulture)));

            this.CreateMap<PerformerImportDto, Performer>();

            this.CreateMap<PerformerSongImportDto, SongPerformer>()
                .ForMember(dest => dest.SongId, opt => opt.MapFrom(src => src.Id));

            this.CreateMap<Song, AlbumSongsExportDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.ToString("F2")))
                .ForMember(dest => dest.WriterName, opt => opt.MapFrom(src => src.Writer.Name));

            this.CreateMap<Album, AlbumExportDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.ToString("F2")))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate.ToString("MM/dd/yyyy")))
                .ForMember(dest => dest.Songs, opt => opt.MapFrom(src => src.Songs
                       .OrderByDescending(s => s.Name).ThenBy(s => s.Writer.Name)));
        }
    }
}