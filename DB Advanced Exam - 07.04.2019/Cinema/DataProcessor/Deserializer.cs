namespace Cinema.DataProcessor
{
    using System;
    using System.IO;
    using System.Text;
    using System.Linq;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    using Data;
    using Cinema.Data.Models;
    using Cinema.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var movieDtos = JsonConvert.DeserializeObject<ImportMovieDto[]>(jsonString,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            var movies = new List<Movie>();

            var result = new StringBuilder();
            foreach (var dto in movieDtos)
            {
                if (IsValid(dto) == false)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                try
                {
                    var movie = AutoMapper.Mapper.Map<Movie>(dto);
                    movies.Add(movie);
                    result.AppendLine(String.Format(SuccessfulImportMovie, movie.Title, movie.Genre, movie.Rating.ToString("F2")));
                }
                catch (Exception)
                {
                    result.AppendLine(ErrorMessage);
                }
            }

            context.Movies.AddRange(movies);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var hallDtos = JsonConvert.DeserializeObject<ImportHallDto[]>(jsonString);

            var result = new StringBuilder();
            var halls = new List<Hall>();
            foreach (var dto in hallDtos)
            {
                if (IsValid(dto) == false)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var hall = AutoMapper.Mapper.Map<Hall>(dto);
                for (int i = 0; i < dto.SeatCount; i++)
                {
                    hall.Seats.Add(new Seat { Hall = hall });
                }

                halls.Add(hall);
                var projectionType = GetProjectionType(hall);
                result.AppendLine(String.Format(SuccessfulImportHallSeat, hall.Name, projectionType, hall.Seats.Count));
            }
            context.Halls.AddRange(halls);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportProjectionDto[]), new XmlRootAttribute("Projections"));

            var projectionDtos = (ImportProjectionDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var movieIds = context.Movies
                .Select(m => m.Id)
                .ToList();
            var hallIds = context.Halls
                .Select(h => h.Id)
                .ToList();

            var projections = new List<Projection>();
            var result = new StringBuilder();
            foreach (var dto in projectionDtos)
            {
                if (hallIds.Contains(dto.HallId) == false || movieIds.Contains(dto.MovieId) == false)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var projection = AutoMapper.Mapper.Map<Projection>(dto);
                projections.Add(projection);
                var movieTitle = context.Movies.FirstOrDefault(m => m.Id == dto.MovieId).Title;
                result.AppendLine(String.Format(SuccessfulImportProjection, movieTitle, projection.DateTime.ToString("MM/dd/yyyy")));
            }

            context.Projections.AddRange(projections);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportCustomerDto[]), new XmlRootAttribute("Customers"));

            var customerDtos = (ImportCustomerDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var customers = new List<Customer>();
            var result = new StringBuilder();
            foreach (var dto in customerDtos)
            {
                if (IsValid(dto) == false)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var customer = AutoMapper.Mapper.Map<Customer>(dto);
                customers.Add(customer);
                result.AppendLine(String.Format(SuccessfulImportCustomerTicket,
                    customer.FirstName, customer.LastName, customer.Tickets.Count));
            }

            context.AddRange(customers);
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

        private static string GetProjectionType(Hall hall)
        {
            if (hall.Is4Dx && hall.Is3D)
            {
                return "4Dx/3D";
            }
            else if (hall.Is4Dx)
            {
                return "4Dx";
            }
            else if (hall.Is3D)
            {
                return "3D";
            }
            return "Normal";
        }
    }
}