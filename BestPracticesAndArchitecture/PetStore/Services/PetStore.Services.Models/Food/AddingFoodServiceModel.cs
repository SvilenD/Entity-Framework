namespace PetStore.Services.Models.Food
{
    using System;

    public class AddingFoodServiceModel
    {
        public string Name { get; set; }
        //in KG
        public double Weight { get; set; }

        public decimal Price { get; set; }

        public double Profit { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }
    }
}