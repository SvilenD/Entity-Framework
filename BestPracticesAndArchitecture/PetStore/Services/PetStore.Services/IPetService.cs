namespace PetStore.Services
{
    using System;
    using System.Collections.Generic;
    using PetStore.Data.Models;
    using PetStore.Services.Models.Pet;

    public interface IPetService
    {
        IEnumerable<PetListingServiceModel> All(int page = 1);

        void BuyPet(Gender gender, DateTime dateOfBirth, decimal price, double profit, string description, int breedId, int categoryId);

        void BuyPet(Pet pet);

        void SellPet(int petId, int orderId);

        bool Exists(int id);

        int Total();
    }
}