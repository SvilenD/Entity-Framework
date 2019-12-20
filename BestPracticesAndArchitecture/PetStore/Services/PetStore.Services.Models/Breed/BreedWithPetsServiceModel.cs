namespace PetStore.Services.Models.Breed
{
    using PetStore.Services.Models.Pet;
    using System.Collections.Generic;

    public class BreedWithPetsServiceModel
    {
        public string Name { get; set; }

        public IEnumerable<PetListingServiceModel> Pets { get; set; }
    }
}