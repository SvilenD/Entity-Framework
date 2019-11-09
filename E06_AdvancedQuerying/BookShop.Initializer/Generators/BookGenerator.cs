namespace BookShop.Initializer.Generators
{
    using BookShop.Models;
    using System;
    using System.Globalization;

    using BookShop.Models.Enums;
    using System.IO;
    using System.Collections.Generic;

    internal class BookGenerator
    {
        #region Book Description
        private static string bookDescription = "Lorem ipsum dolor sit amet  consectetur adipiscing elit  " +
            "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
            "Ut enim ad minim veniam  quis nostrud exercitation ullamco laboris " +
            "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
            "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
            "Excepteur sint occaecat cupidatat non proident  " +
            "sunt in culpa qui officia deserunt mollit anim id est laborum.";
        #endregion

        internal static Book[] CreateBooks()
        {
            var reader = new StreamReader("../../../../BookShop.Initializer/BooksData.txt");

            var booksInput = new List<string>();

            while(reader.EndOfStream)
            {
                booksInput.Add(reader.ReadLine());
            }

            var booksCount = booksInput.Count;

            Category[] categories = CategoryGenerator.CreateCategories();

            Author[] authors = AuthorGenerator.CreateAuthors();

            Book[] books = new Book[booksCount];

            for (int i = 0; i < booksCount; i++)
            {
                var currentBook = booksInput[i].Split();

                var edition = int.Parse(currentBook[0]);

                var releaseDate = DateTime.ParseExact(currentBook[1],  "d/M/yyyy",  CultureInfo.InvariantCulture);

                var copies = int.Parse(currentBook[2]);

                var price = decimal.Parse(currentBook[3]);

                var ageRestriction = int.Parse(currentBook[4]);

                var title = String.Join(" ",  currentBook,  5,  currentBook.Length - 5);

                Category category = categories[i / 10];

                Author author = authors[i / 5];

                Book book = new Book()
                {
                    Title = title, 
                    ReleaseDate = releaseDate, 
                    Description = bookDescription,
                    EditionType = (EditionType)edition, 
                    Price = price, 
                    Copies = copies, 
                    AgeRestriction = (AgeRestriction)ageRestriction, 
                    Author = author 
                };

                BookCategory bookCategory = new BookCategory()
                {
                    Category = category, 
                    Book = book
                };

                book.BookCategories.Add(bookCategory);

                books[i] = book;
            }

            return books;
        }
    }
}