namespace BookShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BookShop.Models.Enums;
    using static ValidationSettings.Book;
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        [MaxLength(TitleMaxLegnth)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public EditionType EditionType { get; set; }

        public decimal Price { get; set; }

        public int Copies { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public AgeRestriction AgeRestriction { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }
             = new HashSet<BookCategory>();
    }
}