namespace PetStore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PetStore.Data;
    using PetStore.Data.Models;
    using PetStore.Services.Implementations;

    public class StartUp
    {
        public static void Main()
        {
            using var data = new PetStoreDbContext();

            //SeedData(data);

            //var foodService = new FoodService(data);
            //foodService.BuyFromDistributor("Unknown", 0.5, 5.5m, 1.2, 10, DateTime.Parse("20/02/2020"), 1, 1);

            //var toyService = new ToyService(data);
            //toyService.BuyFromDistributor("Ball", null, 2, 0.7, 3, 1, 1);

            //var userService = new UserService(data);
            //userService.Register("User2", "User2@gmail.com");

            //var orderService = new OrderService(data, userService);
            //orderService.CreateOrder(1);

            //foodService.SellFoodToUser(new List<int>() { 1 }, 1);
        }

        private static void SeedData(PetStoreDbContext data)
        {
            var breedService = new BreedService(data);
            for (int i = 1; i < 10; i++)
            {
                breedService.Add("Breed " + i);
            }

            var categoryService = new CategoryService(data);
            for (int i = 1; i < 10; i++)
            {
                categoryService.Create("Category " + i);
            }

            var petService = new PetService(data, breedService, categoryService);

            for (int i = 0; i < 50; i++)
            {
                var breedId = data.Breeds
                    .OrderBy(b => Guid.NewGuid())
                    .Select(b => b.Id)
                    .FirstOrDefault();
                var categoryId = data.Categories
                    .OrderBy(c => Guid.NewGuid())
                    .Select(c => c.Id)
                    .FirstOrDefault();

                petService.BuyPet(new Pet()
                {
                    BreedId = breedId,
                    CategoryId = categoryId,
                    DateOfBirth = DateTime.Now.AddDays(2 * i),
                    Gender = (Gender)(i % 2),
                    Price = 10 + 3 * i
                });
            }

            var brandService = new BrandService(data);
            for (int i = 1; i < 10; i++)
            {
                brandService.Add("Brand " + i);
            }

            data.SaveChanges();
        }

    }
}