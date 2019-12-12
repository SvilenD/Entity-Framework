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

            brandService.Create("Purrina");
        }
    }
}