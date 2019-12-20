namespace PetStore.Services
{
    using System;
    using PetStore.Data.Models;

    public interface IPetService
    {
        void BuyPet(Gender gender, DateTime DateOfBirth, decimal Price, double profit, string description, int breedId, int categoryId);
    }
}