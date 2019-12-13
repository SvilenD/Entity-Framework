namespace PetStore.Services.Models.Food
{
    using System;

    public class FoodListingServiceModel
    {
        public string Name { get; set; }

        public string Brand { get; set; }

        //in KG
        public double Weight { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int TotalOrders { get; set; }
    }
}