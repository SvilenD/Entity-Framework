namespace PetStore.Services.Implementations
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Data;
    using PetStore.Data.Models;
    using PetStore.Services.Models.Brand;
    using PetStore.Services.Models.Toy;

    using static Validator;

    public class BrandService : IBrandService
    {
        private readonly PetStoreDbContext data;

        public BrandService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public int Create(string name)
        {
            if (this.data.Brands.Any(br => br.Name == name))
            {
                throw new InvalidOperationException(String.Format(OutputMessages.BrandAlreadyExists, name));
            }

            var brand = new Brand { Name = name };

            if (IsValid(brand) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidBrand);
            }

            this.data.Brands.Add(brand);
            this.data.SaveChanges();

            return brand.Id;
        }

        public BrandWithToysServiceModel FindByIdWithToys(int id)
            => this.data.Brands
                .Where(br => br.Id == id)
                .Select(br => new BrandWithToysServiceModel
                {
                    Name = br.Name,
                    Toys = br.Toys.Select(t => new ToyListingServiceModel
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Price = t.Price,
                        TotalOrders = t.Orders.Count
                    })
                })
                .FirstOrDefault();

        public IEnumerable<BrandListingServiceModel> SearchByName(string name)
            => this.data.Brands
                .Where(b => b.Name.ToLower().Contains(name.ToLower()))
                .Select(br => new BrandListingServiceModel
                {
                    Id = br.Id,
                    Name = br.Name
                })
                .ToList();
    }
}