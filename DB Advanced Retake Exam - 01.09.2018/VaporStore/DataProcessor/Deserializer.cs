namespace VaporStore.DataProcessor
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using System.Globalization;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    using Data;
    using VaporStore.DataProcessor.ImportDtos;
    using VaporStore.Data.Models;
    using static OutputConstants;
    using System.Xml.Serialization;
    using System.IO;
    using VaporStore.Data.Models.Enums;

    public static class Deserializer
    {
        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var deserializedGames = JsonConvert.DeserializeObject<GameImportDto[]>(jsonString);

            var developers = new HashSet<Developer>();
            var genres = new HashSet<Genre>();
            var tags = new HashSet<Tag>();
            var games = new HashSet<Game>();

            var result = new StringBuilder();
            foreach (var gameDto in deserializedGames)
            {
                if (!IsValid(gameDto) || !gameDto.Tags.All(IsValid))
                {
                    result.AppendLine(ErrorMsg);
                    continue;
                }

                var developer = GetDeveloper(developers, gameDto.Developer);

                var genre = GetGenre(genres, gameDto.Genre);

                var gameTags = GetTags(tags, gameDto.Tags);

                var game = new Game
                {
                    Name = gameDto.Name,
                    Price = gameDto.Price,
                    ReleaseDate = DateTime.ParseExact(gameDto.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Developer = developer,
                    Genre = genre,
                    GameTags = gameTags
                        .Select(tag => new GameTag { Tag = tag }).ToHashSet()
                };

                games.Add(game);

                result.AppendLine(String.Format(GameAddedMsg, gameDto.Name, gameDto.Genre, gameDto.Tags.Count));
            }

            context.Games.AddRange(games);

            context.SaveChanges();

            return result.ToString();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var userDtos = JsonConvert.DeserializeObject<UserImportDto[]>(jsonString);

            var users = new HashSet<User>();
            var result = new StringBuilder();
            foreach (var dto in userDtos)
            {
                if (IsValid(dto) == false)
                {
                    result.AppendLine(ErrorMsg);
                    continue;
                }

                var user = AutoMapper.Mapper.Map<User>(dto);
                users.Add(user);
                result.AppendLine(String.Format(UserAddedMsg, user.Username, user.Cards.Count));
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(PurchaseImportDto[]), new XmlRootAttribute("Purchases"));

            var purchaseDtos = (PurchaseImportDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var purchases = new HashSet<Purchase>();
            var result = new StringBuilder();
            var cards = context.Cards.Include(c=>c.User).ToHashSet();
            var games = context.Games.ToHashSet();
            foreach (var dto in purchaseDtos)
            {
                if (IsValid(dto) == false)
                {
                    result.AppendLine(ErrorMsg);
                    continue;
                }

                try
                {
                    var card = cards.FirstOrDefault(c => c.Number == dto.CardNumber);
                    var purchase = new Purchase
                    {
                        Game = games.FirstOrDefault(g => g.Name == dto.GameName),
                        Card = card,
                        Date = DateTime.ParseExact(dto.Date, @"dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                        ProductKey = dto.ProductKey,
                        Type = Enum.Parse<PurchaseType>(dto.Type, true)
                    };
                    purchases.Add(purchase);
                    result.AppendLine(String.Format(PurchaseImportMsg, dto.GameName, card.User.Username));
                }
                catch (Exception)
                {
                    result.AppendLine(ErrorMsg);
                    continue;
                }
            }

            context.Purchases.AddRange(purchases);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        private static Developer GetDeveloper(HashSet<Developer> developers, string developerName)
        {
            var developer = developers.FirstOrDefault(d => d.Name == developerName);
            if (developer == null)
            {
                developer = new Developer { Name = developerName };
                developers.Add(developer);
            }

            return developer;
        }

        private static ICollection<Tag> GetTags(HashSet<Tag> tags, ICollection<string> tagNames)
        {
            var gameTags = new HashSet<Tag>();
            foreach (var tagName in tagNames)
            {
                var tag = tags.SingleOrDefault(t => t.Name == tagName);
                if (tag == null)
                {
                    tag = new Tag { Name = tagName };
                    tags.Add(tag);
                }

                gameTags.Add(tag);
            }

            return gameTags;
        }

        private static Genre GetGenre(HashSet<Genre> genres, string genreName)
        {
            var currentGenre = genres.FirstOrDefault(g => g.Name == genreName);
            if (currentGenre == null)
            {
                currentGenre = new Genre
                {
                    Name = genreName
                };
                genres.Add(currentGenre);
            }

            return currentGenre;
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