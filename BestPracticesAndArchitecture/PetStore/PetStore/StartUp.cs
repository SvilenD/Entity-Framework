namespace PetStore
{
    using System;
    using PetStore.Data;
    using PetStore.Services.Implementations;

    public class StartUp
    {
        public static void Main()
        {
            using var data = new PetStoreDbContext();

            var brandService = new BrandService(data);

            var foodService = new FoodService(data);

            //foodService.BuyFromDistributor("Unknown", 0.5, 5.5m, 1.2, DateTime.Parse("20/02/2020"), 1, 1);
            var toyService = new ToyService(data);

            //toyService.BuyFromDistributor("Ball", null, 2, 0.7, 1, 1);
        }
    }
}