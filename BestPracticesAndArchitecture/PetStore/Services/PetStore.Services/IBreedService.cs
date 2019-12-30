namespace PetStore.Services
{
    using PetStore.Services.Models.Breed;
    using System.Collections.Generic;

    public interface IBreedService
    {
        int Add(string name);

        bool Exists(string name);

        bool Exists(int id);

        IEnumerable<BreedListingServiceModel> SearchByName(string name);

        BreedWithPetsServiceModel FindByIdWithPets(int id);
    }
}