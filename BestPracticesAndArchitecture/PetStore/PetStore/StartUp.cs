namespace PetStore
{
    using System;
    using System.Collections.Generic;
    using PetStore.Data;
    using PetStore.Services.Implementations;

    public class StartUp
    {
        public static void Main()
        {
            using var data = new PetStoreDbContext();

            var brandService = new BrandService(data);
            brandService.Create("Whiskas");

            var foodService = new FoodService(data);
            foodService.BuyFromDistributor("Unknown", 0.5, 5.5m, 1.2, 10, DateTime.Parse("20/02/2020"), 1, 1);

            var toyService = new ToyService(data);
            toyService.BuyFromDistributor("Ball", null, 2, 0.7, 3, 1, 1);

            var userService = new UserService(data);
            userService.Register("User1", "User1@gmail.com");

            var orderService = new OrderService(data, userService);
            orderService.CreateOrder(1);

            foodService.SellFoodToUser(new List<int>() { 1 }, 1);
        }
    }
}