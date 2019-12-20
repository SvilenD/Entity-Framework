namespace PetStore.Services
{
    using System.Collections.Generic;
    using PetStore.Services.Models.Toy;

    public interface IToyService
    {
        void BuyFromDistributor(string name, string description, decimal price, double profit, int quantity, int brandId, int categoryId);

        void BuyFromDistributor(AddingToyServiceModel toyServiceModel);

        IEnumerable<ToyListingServiceModel> SearchByName(string name);

        void SellToysToUser(List<int> toyIds, int orderId);
    }
}