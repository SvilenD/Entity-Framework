namespace PetStore.Services
{
    using PetStore.Services.Models.Breed;
    using System.Collections.Generic;

    public interface IBreedService
    {
        int Create(string name);

        bool Exists(string name);

        IEnumerable<BreedListingServiceModel> SearchByName(string name);

        BreedWithPetsServiceModel FindByIdWithPets(int id);
    }
}