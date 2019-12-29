namespace PetStore.Services.Implementations
{
    using System;
    using System.Linq;

    using PetStore.Data;
    using PetStore.Data.Models;
    using static Validator;
    
    public class PetService : IPetService
    {
        private readonly PetStoreDbContext data;

        public PetService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public void BuyPet(Gender gender, DateTime dateOfBirth, decimal price, double profit, string description, int breedId, int categoryId)
        {
            if (this.data.Breeds.Any(b => b.Id == breedId) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidBreed);
            }
            else if (this.data.Categories.Any(c => c.Id == categoryId) == false)
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

        public bool Exists(int id)
            => this.data.Pets.Any(p => p.Id == id);

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
    }
}