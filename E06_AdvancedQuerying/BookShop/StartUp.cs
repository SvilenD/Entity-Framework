namespace BookShop
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Globalization;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Initializer;
    using BookShop.Models.Enums;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                DbInitializer.ResetDatabase(db);

                Console.WriteLine(RemoveBooks(db));
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
            var result = new StringBuilder();

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

            var result = new StringBuilder();

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

            var result = new StringBuilder();

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
                    TotalCopies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(b => b.TotalCopies)
                .ToList();

            var result = new StringBuilder();

            foreach (var author in authorsInfo)
            {
                result.AppendLine($"{author.Name} - {author.TotalCopies}");
            }

            return result.ToString().TrimEnd();
        }

        //Task 12
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    Profit = c.CategoryBooks.Sum(b => b.Book.Copies * b.Book.Price)
                })
                .OrderByDescending(c => c.Profit)
                .ThenBy(c => c.Name)
                .ToArray();


            var result = new StringBuilder();

            foreach (var cat in categories)
            {
                result.AppendLine($"{cat.Name} ${cat.Profit:F2}");
            }

            return result.ToString().TrimEnd();
        }

        //Task 13
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var booksByCategory = context.Categories
                .Select(c => new
                {
                    c.Name,
                    Books = c.CategoryBooks.Select(b => new
                    {
                        b.Book.Title,
                        ReleaseDate = b.Book.ReleaseDate.Value
                    })
                    .OrderByDescending(b => b.ReleaseDate)
                    .Take(3)
                })
                .OrderBy(c => c.Name)
                .ToArray();

            var result = new StringBuilder();

            foreach (var cat in booksByCategory)
            {
                result.AppendLine($"--{cat.Name}");
                foreach (var book in cat.Books)
                {
                    result.AppendLine($"{book.Title} ({book.ReleaseDate.Year})");
                }
            }

            return result.ToString().TrimEnd();
        }

        //Task14
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010).ToArray();

            foreach (var book in books)
            {
                book.Price += 5;
            }
            context.SaveChanges();
        }

        //Task15
        public static int RemoveBooks(BookShopContext context)
        {
            var count = context.Books.Count(b => b.Copies < 4200);

            context
                .Books
                .RemoveRange(context.Books.Where(b => b.Copies < 4200)
                .ToArray());

            context.SaveChanges();

            return count;
        }
    }
}