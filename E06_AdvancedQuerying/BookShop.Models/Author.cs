namespace BookShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ValidationSettings.Author;

    public class Author
    {
        public int AuthorId { get; set; }

        [MaxLength(NameMaxLegnth)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(NameMaxLegnth)]
        public string LastName { get; set; }

        public ICollection<Book> Books { get; set; }
            = new HashSet<Book>();
    }
}