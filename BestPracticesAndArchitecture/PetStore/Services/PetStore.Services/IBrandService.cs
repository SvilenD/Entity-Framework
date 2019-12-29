namespace PetStore.Services
{
    using System.Collections.Generic;

    using Models.Brand;

    public interface IBrandService
    {
        int Add(string name);

        IEnumerable<BrandListingServiceModel> SearchByName(string name);

        BrandWithToysServiceModel FindByIdWithToys(int id);

        bool Exists(int brandId);
    }
}