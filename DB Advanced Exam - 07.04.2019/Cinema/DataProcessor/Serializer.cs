namespace Cinema.DataProcessor
{
    using System;
    using System.Linq;

    using Data;
    using Cinema.DataProcessor.ExportDto;
    using Newtonsoft.Json;
    using System.Xml.Serialization;
    using System.Text;
    using System.IO;
    using System.Xml;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var topMovies = context.Movies
                .Where(r => r.Rating >= rating && r.Projections.Any(t => t.Tickets.Count >= 1))
                .OrderByDescending(m => m.Rating)
                .ThenByDescending(m => m.Projections.Sum(p => p.Tickets.Sum(t => t.Price)))
                .Select(m => new ExportMovieDto
                {
                    Title = m.Title,
                    Rating = m.Rating.ToString("F2"),
                    TotalIncomes = m.Projections.Sum(p => p.Tickets.Sum(t => t.Price)).ToString("F2"),
                    Customers = m.Projections
                        .SelectMany(p => p.Tickets)
                        .Select(c => new ExportMovieCustomerDto
                        {
                            FirstName = c.Customer.FirstName,
                            LastName = c.Customer.LastName,
                            Balance = c.Customer.Balance.ToString("F2")
                        })
                        .OrderByDescending(c => c.Balance)
                        .ThenBy(c => c.FirstName)
                        .ThenBy(c => c.LastName)
                        .ToList()
                })
                .Take(10)
                .ToArray();

            var result = JsonConvert.SerializeObject(topMovies, Newtonsoft.Json.Formatting.Indented);

            return result;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            var customers = context.Customers
                .Where(c => c.Age >= age)
                .OrderByDescending(c => c.Tickets.Sum(p => p.Price))
                .Take(10)
                .Select(c => new ExportCustomerDto
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    SpentMoney = c.Tickets.Sum(p => p.Price).ToString("F2"),
                    SpentTime = TimeSpan.FromSeconds(c.Tickets
                        .Sum(t => t.Projection.Movie.Duration.TotalSeconds)).ToString(@"hh\:mm\:ss")
                })
                .ToArray();

            var xmlSerializer = new XmlSerializer(customers.GetType(), new XmlRootAttribute("Customers"));

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var result = new StringBuilder();

            xmlSerializer.Serialize(new StringWriter(result), customers, namespaces);

            return result.ToString().TrimEnd();
        }
    }
}