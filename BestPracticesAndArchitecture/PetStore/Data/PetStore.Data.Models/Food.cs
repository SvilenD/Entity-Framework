namespace PetStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Models.Validations;
    using static Validations.DataValidation;

    public class Food
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }
        //in KG
        [Range(WeightMinValue, WeightMaxValue)]
        public double Weight { get; set; }

        [Range(0, double.MaxValue)]
        public decimal DistributorPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [CustomDateAttribute]
        public DateTime ExpirationDate { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<FoodOrder> Orders { get; set; } = new HashSet<FoodOrder>();
    }
}