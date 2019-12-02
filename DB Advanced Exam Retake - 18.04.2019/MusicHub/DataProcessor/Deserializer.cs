namespace MusicHub.DataProcessor
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    using Data;
    using MusicHub.Data.Models;
    using MusicHub.DataProcessor.ImportDtos;
    using System.Xml.Serialization;
    using System.IO;
    using MusicHub.Data.Models.Enums;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data";

        private const string SuccessfullyImportedWriter
            = "Imported {0}";
        private const string SuccessfullyImportedProducerWithPhone
            = "Imported {0} with phone: {1} produces {2} albums";
        private const string SuccessfullyImportedProducerWithNoPhone
            = "Imported {0} with no phone number produces {1} albums";
        private const string SuccessfullyImportedSong
            = "Imported {0} ({1} genre) with duration {2}";
        private const string SuccessfullyImportedPerformer
            = "Imported {0} ({1} songs)";

        public static string ImportWriters(MusicHubDbContext context, string jsonString)
        {
            var writerDtos = JsonConvert.DeserializeObject<WriterImportDto[]>(jsonString);

            var writers = new List<Writer>();
            var result = new StringBuilder();

            foreach (var dto in writerDtos)
            {
                if (IsValid(dto) == false)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }
                var currentWriter = AutoMapper.Mapper.Map<Writer>(dto);

                writers.Add(currentWriter);
                result.AppendLine(String.Format(SuccessfullyImportedWriter, dto.Name));
            }

            context.Writers.AddRange(writers);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }


        public static string ImportProducersAlbums(MusicHubDbContext context, string jsonString)
        {
            var producerAlbumsDtos = JsonConvert.DeserializeObject<ProducerImportDto[]>(jsonString);

            var producers = new List<Producer>();
            var result = new StringBuilder();

            foreach (var dto in producerAlbumsDtos)
            {
                if (IsValid(dto) == false || dto.Albums.All(IsValid) == false)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var producer = AutoMapper.Mapper.Map<Producer>(dto);
                producers.Add(producer);
                if (producer.PhoneNumber == null)
                {
                    result.AppendLine(String
                        .Format(SuccessfullyImportedProducerWithNoPhone, producer.Name, producer.Albums.Count()));
                }
                else
                {
                    result.AppendLine(String
                        .Format(SuccessfullyImportedProducerWithPhone, producer.Name, producer.PhoneNumber, producer.Albums.Count()));
                }
            }

            context.Producers.AddRange(producers);
            context.SaveChanges();

            return result.ToString().Trim();
        }

        public static string ImportSongs(MusicHubDbContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(SongImportDto[]), new XmlRootAttribute("Songs"));
            var songDtos = (SongImportDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var result = new StringBuilder();
            var validSongs = new List<Song>();

            foreach (var songDto in songDtos)
            {
                if (IsValid(songDto) == false)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var genre = Enum.TryParse(songDto.Genre, out Genre genreResult);
                var album = context.Albums.Find(songDto.AlbumId);
                var writer = context.Writers.Find(songDto.WriterId);
                var songTitle = validSongs.Any(s => s.Name == songDto.Name);

                if (genre == false || album == null || writer == null || songTitle)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var song = AutoMapper.Mapper.Map<Song>(songDto);

                result.AppendLine(String.Format(SuccessfullyImportedSong, song.Name, song.Genre, song.Duration));
                validSongs.Add(song);
            }

            context.Songs.AddRange(validSongs);
            context.SaveChanges();

            return result.ToString().Trim();
        }

        public static string ImportSongPerformers(MusicHubDbContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(PerformerImportDto[]), new XmlRootAttribute("Performers"));
            var performerDtos = (PerformerImportDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var result = new StringBuilder();
            var validSongIds = context.Songs.Select(s => s.Id).ToList();
            var performers = new List<Performer>();

            foreach (var dto in performerDtos)
            {
                if (IsValid(dto) == false)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                bool hasInvalidSongs = false;
                foreach (var song in dto.PerformerSongs)
                {
                    if (validSongIds.Contains(song.Id) == false)
                    {
                        result.AppendLine(ErrorMessage);
                        hasInvalidSongs = true;
                        break;
                    }
                }

                if (hasInvalidSongs == false)
                {
                    var performer = AutoMapper.Mapper.Map<Performer>(dto);
                    performers.Add(performer);
                    result.AppendLine(String
                        .Format(SuccessfullyImportedPerformer, performer.FirstName, performer.PerformerSongs.Count));
                }
            }

            context.Performers.AddRange(performers);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResult = new List<ValidationResult>();

            var result = Validator.TryValidateObject(entity, validationContext, validationResult, true);

            return result;
        }
    }
}