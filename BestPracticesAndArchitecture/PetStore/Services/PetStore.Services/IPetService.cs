namespace PetStore.Services
{
    using System;
    using PetStore.Data.Models;

    public interface IPetService
    {
        void BuyPet(Gender gender, DateTime dateOfBirth, decimal price, double profit, string description, int breedId, int categoryId);

        void SellPet(int petId, int orderId);

        bool Exists(int id);
    }
}