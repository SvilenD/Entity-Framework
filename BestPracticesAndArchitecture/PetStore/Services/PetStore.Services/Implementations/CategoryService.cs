namespace PetStore.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using PetStore.Data;
    using PetStore.Data.Models;
    using PetStore.Services.Models.Category;
    using static Validator;

    public class CategoryService : ICategoryService
    {
        private readonly PetStoreDbContext data;

        public CategoryService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public int Create(string name)
        {
            var category = new Category()
            {
                Name = name
            };

            if (IsValid(category) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidCategory);
            }
            this.data.Categories.Add(category);

            return category.Id;
        }

        public IEnumerable<CategoryListingServiceModel> SearchByName(string name)
        {
            return this.data.Categories
                .Where(c => c.Name.ToLower().Contains(name.ToLower()))
                .Select(c => new CategoryListingServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToList();
        }

        public void AddDescription(string name, string description)
        {
            var category = this.data.Categories.FirstOrDefault(c => c.Name.ToLower() == name.ToLower());

            category.Description = description;

            this.data.SaveChanges();
        }
    }
}