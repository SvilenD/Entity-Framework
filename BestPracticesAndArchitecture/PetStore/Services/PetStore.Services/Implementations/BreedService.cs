namespace PetStore.Services.Implementations
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using PetStore.Data;
    using PetStore.Services.Models.Breed;
    using PetStore.Data.Models;
    using PetStore.Services.Models.Pet;
    using static Validator;

    public class BreedService : IBreedService
    {
        private readonly PetStoreDbContext data;

        public BreedService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public int Add(string name)
        {
            if (this.Exists(name))
            {
                throw new InvalidOperationException(String.Format(OutputMessages.BreedAlreadyExists, name));
            }

            var breed = new Breed() { Name = name };

            if (IsValid(breed) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidBreed);
            }

            this.data.Breeds.Add(breed);
            this.data.SaveChanges();

            return breed.Id;
        }

        public bool Exists(string name)
            => this.data.Breeds
            .Any(b => b.Name.ToLower() == name.ToLower());

        public bool Exists(int id)
            => this.data.Breeds
            .Any(b => b.Id == id);

        public BreedWithPetsServiceModel FindByIdWithPets(int id)
            => this.data.Breeds
                .Where(b => b.Id == id)
                .Select(b => new BreedWithPetsServiceModel()
                {
                    Name = b.Name,
                    Pets = b.Pets.Select(p => new PetListingServiceModel()
                    {
                        Id = p.Id,
                        DateOfBirth = p.DateOfBirth.ToShortDateString(),
                        Gender = p.Gender.ToString(),
                        Price = p.Price,
                        Description = p.Description ?? string.Empty
                    }).ToList()
                })
                .FirstOrDefault();

        public IEnumerable<BreedListingServiceModel> SearchByName(string name)
            => this.data.Breeds
                .Where(b => b.Name.ToLower().Contains(name.ToLower()))
                .Select(b => new BreedListingServiceModel()
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToList();
    }
}