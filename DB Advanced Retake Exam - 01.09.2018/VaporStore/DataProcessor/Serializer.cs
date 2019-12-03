namespace VaporStore.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using VaporStore.DataProcessor.ExportDtos;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var genreDtos = context.Genres
                .Where(g => genreNames.Contains(g.Name))
                .Select(g => new GenreExportDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    Games = g.Games.Where(ga => ga.Purchases.Any())
                        .Select(ga => new GameExportDto
                        {
                            Id = ga.Id,
                            Name = ga.Name,
                            Developer = ga.Developer.Name,
                            Tags = String.Join(", ", ga.GameTags.Select(t => t.Tag.Name)),
                            PlayersCount = ga.Purchases.Count
                        })
                        .OrderByDescending(ga => ga.PlayersCount)
                        .ThenBy(ga => ga.Id)
                        .ToArray(),
                    TotalPlayers = g.Games.Sum(ga => ga.Purchases.Count)
                })
                .OrderByDescending(g => g.TotalPlayers)
                .ThenBy(g => g.Id)
                .ToArray();

            var genresJson = JsonConvert.SerializeObject(genreDtos, Newtonsoft.Json.Formatting.Indented);

            //using (var writer = new StreamWriter("../../../genresJson.json"))
            //{
            //    writer.Write(genresJson);
            //}

            return genresJson;
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            var userDtos = context.Users
                .Select(u => new UserExportDto
                {
                    Username = u.Username,
                    TotalSpent = u.Cards
                        .SelectMany(c => c.Purchases)
                        .Where(p => p.Type.ToString() == storeType)
                        .Sum(p => p.Game.Price),
                    Purchases = u.Cards
                        .SelectMany(c => c.Purchases
                        .Where(p => p.Type.ToString() == storeType)
                        .Select(p => new PurchaseExportDto
                        {
                            CardNumber = c.Number,
                            Cvc = c.Cvc,
                            Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                            Game = new PurchaseGameExportDto
                            {
                                Name = p.Game.Name,
                                Genre = p.Game.Genre.Name,
                                Price = p.Game.Price
                            }
                        }))
                        .OrderBy(p => p.Date)
                        .ToArray()
                })
                .Where(u => u.Purchases.Any())
                .OrderByDescending(u => u.TotalSpent)
                .ThenBy(u => u.Username)
                .ToArray();

            var xmlSerializer = new XmlSerializer(userDtos.GetType(), new XmlRootAttribute("Users"));

            var result = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            xmlSerializer.Serialize(new StringWriter(result), userDtos, namespaces);
            //xmlSerializer.Serialize(new StreamWriter("../../../user.xml"), userDtos, namespaces);

            return result.ToString().TrimEnd();
        }
    }
}