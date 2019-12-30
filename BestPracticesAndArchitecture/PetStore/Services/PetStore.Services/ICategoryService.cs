namespace PetStore.Services
{
    using PetStore.Services.Models.Category;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        int Create(string name);

        bool Exists(string name);

        bool Exists(int id);

        void AddDescription(string name, string description);

        IEnumerable<CategoryListingServiceModel> SearchByName(string name);
    }
}