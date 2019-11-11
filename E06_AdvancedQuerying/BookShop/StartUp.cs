namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static StringBuilder result = new StringBuilder();

        public static void Main()
        {
            //var input = int.Parse(Console.ReadLine());

            using (var db = new BookShopContext())
            {
                //    DbInitializer.ResetDatabase(db);

                Console.WriteLine(CountCopiesByAuthor(db));
            }
        }

        //Task 1
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var ageRestriction = Enum.Parse<AgeRestriction>(command, true);

            var books = context
                .Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        //Task 2
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context
                 .Books
                 .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                 .OrderBy(b => b.BookId)
                 .Select(b => b.Title)
                 .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        //Task 3
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context
                .Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .OrderByDescending(b => b.Price)
                .ToArray();

            for (int i = 0; i < books.Length; i++)
            {
                result.AppendLine($"{books[i].Title} - ${books[i].Price:f2}");
            }

            return result.ToString().Trim();
        }

        //Task 4
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return String.Join(Environment.NewLine, books);
        }

        //Task 5 
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var books = context.Books
                .Where(b => b.BookCategories
                    .Select(bc => new
                    {
                        Category = bc.Category.Name.ToLower()
                    })
                        .Any(c => categories.Contains(c.Category)))
                .Select(b => b.Title)
                .OrderBy(t => t);

            return String.Join(Environment.NewLine, books);
        }

        //Task 6 
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime dateLimit = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(b => b.ReleaseDate < dateLimit)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price
                })
                .ToList();

            foreach (var book in books)
            {
                result.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }

            return result.ToString().TrimEnd();
        }

        //Task 7
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => a.FirstName + " " + a.LastName)
                .OrderBy(a => a)
                .ToArray();

            return String.Join(Environment.NewLine, authors);
        }

        //Task 8 
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToArray();

            return String.Join(Environment.NewLine, books);
        }

        //Task 9
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => EF.Functions.Like(b.Author.LastName, $"{input}%"))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    Title = b.Title,
                    Author = b.Author.FirstName + " " + b.Author.LastName
                })
                .ToList();

            foreach (var book in books)
            {
                result.AppendLine($"{book.Title} ({book.Author})");
            }

            return result.ToString().TrimEnd();
        }

        //Task 10
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return context.Books.Where(b => b.Title.Length > lengthCheck).Count();
        }

        //Task 11
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authorsInfo = context.Authors
                .Select(a => new
                {
                    Name = a.FirstName + " " + a.LastName,
                    CopiesCount = a.Books.Sum(c => c.Copies)
                })
                .OrderByDescending(a=>a.CopiesCount)
                .ToArray();

            foreach (var author in authorsInfo)
            {
                result.AppendLine($"{author.Name} - {author.CopiesCount}");
            }

            return result.ToString().TrimEnd();
        }
    }
}