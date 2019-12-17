namespace PetStore.Services
{
    using PetStore.Services.Models.Category;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        int Create(string name);

        IEnumerable<CategoryListingServiceModel> SearchByName(string name);
    }
}