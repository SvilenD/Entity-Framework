namespace PetStore.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using PetStore.Data;
    using PetStore.Data.Models;
    using PetStore.Services.Models.Pet;
    using static Validator;

    public class PetService : IPetService
    {
        private const int PetsPerPage = 20;

        private readonly PetStoreDbContext data;
        private readonly IBreedService breedService;
        private readonly ICategoryService categoryService;

        public PetService(PetStoreDbContext data, IBreedService breedService, ICategoryService categoryService)
        {
            this.data = data;
            this.breedService = breedService;
            this.categoryService = categoryService;
        }

        public void BuyPet(Gender gender, DateTime dateOfBirth, decimal price, double profit, string description, int breedId, int categoryId)
        {
            if (this.breedService.Exists(breedId) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidBreed);
            }
            else if (!this.categoryService.Exists(categoryId) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidCategory);
            }

            var pet = new Pet()
            {
                Gender = gender,
                DateOfBirth = dateOfBirth,
                DistributorPrice = price,
                Price = price + (price * (decimal)profit),
                Description = description ?? string.Empty,
                BreedId = breedId,
                CategoryId = categoryId
            };
            
            if (IsValid(pet) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidPet);
            }

            this.data.Pets.Add(pet);
            this.data.SaveChanges();
        }

        public void BuyPet(Pet pet)
        {
            if (IsValid(pet) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidPet);
            }

            this.data.Pets.Add(pet);
            this.data.SaveChanges();
        }

        public bool Exists(int id)
            => this.data.Pets.Any(p => p.Id == id);

        public IEnumerable<PetListingServiceModel> All(int page = 1)
         => this.data
            .Pets
            .Skip((page - 1) * PetsPerPage)
            .Take(PetsPerPage)
            .Select(p => new PetListingServiceModel()
            {
                Id = p.Id,
                Category = p.Category.Name,
                Breed = p.Breed.Name,
                Gender = p.Gender.ToString(),
                DateOfBirth = p.DateOfBirth.ToShortDateString(),
                Price = p.Price,
                Description = p.Description
            })
            .ToList();

        public void SellPet(int petId, int orderId)
        {
            if (this.Exists(petId) == false)
            {
                throw new ArgumentNullException(String.Format(OutputMessages.PetNotExists, petId));
            }

            var order = this.data.Orders.Find(orderId);
            if (order == null)
            {
                throw new ArgumentNullException(String.Format(OutputMessages.OrderNotExists, orderId));
            }

            var pet = this.data.Pets.Find(petId);
            pet.OrderId = orderId;
            order.Pets.Add(pet);
            this.data.SaveChanges();
        }

        public int Total() => this.data.Pets.Count();
    }
}