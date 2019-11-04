namespace P03_SalesDatabase.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Column(TypeName = "NVARCHAR(50)")]
        public string Name { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public ICollection<Sale> Sales { get; set; }
            = new HashSet<Sale>(); 
    }
}
