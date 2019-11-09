namespace BookShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ValidationSettings.Category;

    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(NameMaxLegnth)]
        public string Name { get; set; }

        public ICollection<BookCategory> CategoryBooks { get; set; }
            = new HashSet<BookCategory>();
    }
}