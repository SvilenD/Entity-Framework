namespace PetStore.Services
{
    using System;
    using System.Collections.Generic;
    using PetStore.Services.Models.Food;

    public interface IFoodService
    {
        void BuyFromDistributor(string name, double weight, decimal price, double profit, int quantity, DateTime expiration, int brandId, int categoryId);

        void BuyFromDistributor(AddingFoodServiceModel foodServiceModel);

        IEnumerable<FoodListingServiceModel> SearchByName(string name);

        void SellFoodToUser(List<int> foodIds, int orderId);
    }
}