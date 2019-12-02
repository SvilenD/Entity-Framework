namespace MusicHub.DataProcessor
{
    using System.Xml;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;
    using AutoMapper.QueryableExtensions;
    using System.Xml.Serialization;

    using Data;
    using MusicHub.DataProcessor.ExportDtos;
    using System.IO;

    public class Serializer
    {
        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var producerAlbums = context.Albums
                .Where(a => a.ProducerId == producerId)
                .OrderByDescending(a => a.Price)
                .ProjectTo<AlbumExportDto>()
                .ToArray();

            var producerAlbumsJson = JsonConvert.SerializeObject(producerAlbums, Newtonsoft.Json.Formatting.Indented);

            return producerAlbumsJson;
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var songsWithDuration = context.Songs
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(s=> new SongExportDto
                {
                    SongName = s.Name,
                    AlbumProducer = s.Album.Producer.Name,
                    Duration = s.Duration.ToString("c"),
                    Writer = s.Writer.Name,
                    Performer = s.SongPerformers.Select(p => p.Performer.FirstName + " " + p.Performer.LastName).FirstOrDefault()
                })
                .OrderBy(s => s.SongName)
                .ThenBy(s => s.Writer)
                .ThenBy(s => s.Performer)
                .ToArray();

            var attr = new XmlRootAttribute("Songs");
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var serializer = new XmlSerializer(songsWithDuration.GetType(), attr);
            var result = new StringBuilder();
            serializer.Serialize(new StringWriter(result), songsWithDuration, namespaces);

            return result.ToString().TrimEnd();
        }
    }
}