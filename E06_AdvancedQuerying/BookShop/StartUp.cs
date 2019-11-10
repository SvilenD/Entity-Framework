namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            //using (var db = new BookShopContext())
            //{
            //    DbInitializer.ResetDatabase(db);
            //}

            using (var db = new BookShopContext())
            {
                Console.WriteLine(GetGoldenBooks(db));
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
                 .Where(b => b.EditionType == EditionType.Gold &&  b.Copies < 5000)
                 .OrderBy(b=>b.BookId)
                 .Select(b => b.Title)
                 .ToArray();

            return string.Join(Environment.NewLine, books);
        }
    }
}