namespace PetStore.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using Data.Models;
    using Services.Models.Food;

    using static Validator;

    public class FoodService : IFoodService
    {
        private readonly PetStoreDbContext data;

        public FoodService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public void BuyFromDistributor(string name, double weight, decimal price, double profit, int quantity, DateTime expiration, int brandId, int categoryId)
        {
            if (this.data.Brands.Any(b => b.Id == brandId) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidBrand);
            }
            else if (this.data.Categories.Any(c => c.Id == categoryId) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidCategory);
            }

            var food = CreateFood(name, weight, price, profit, quantity, expiration, brandId, categoryId);

            if (IsValid(food) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidFood);
            }

            this.data.Foods.Add(food);
            this.data.SaveChanges();
        }

        public void BuyFromDistributor(AddingFoodServiceModel model)
        {
            if (this.data.Brands.Any(b => b.Id == model.BrandId) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidBrand);
            }
            else if (this.data.Categories.Any(c => c.Id == model.CategoryId) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidCategory);
            }

            var food = CreateFood(model.Name, model.Weight, model.Price, model.Profit, model.Quantity, model.ExpirationDate, model.BrandId, model.CategoryId);

            if (IsValid(food) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidFood);
            }

            this.data.Foods.Add(food);
            this.data.SaveChanges();
        }

        public bool Exists(int foodId)
            => this.data.Foods.Any(f => f.Id == foodId);

        public IEnumerable<FoodListingServiceModel> SearchByName(string name)
        {
            return this.data.Foods
                .Where(f => f.Name.ToLower().Contains(name.ToLower()))
                .Select(f => new FoodListingServiceModel
                {
                    Name = f.Name,
                    Brand = f.Brand.Name,
                    ExpirationDate = f.ExpirationDate,
                    Weight = f.Weight,
                    Price = f.Price,
                    TotalOrders = f.Orders.Count
                }).ToList();
        }

        public void SellFoodToUser(List<int> foodIds, int orderId)
        {
            var order = this.data.Orders.Find(orderId);
            if (order == null)
            {
                throw new ArgumentNullException(String.Format(OutputMessages.OrderNotExists, orderId));
            }

            foreach (var id in foodIds)
            {
                var food = this.data.Foods.Find(id);

                if (food == null || food.Quantity < 1)
                {
                    throw new InvalidOperationException(String.Format(OutputMessages.FoodNotExists, id));
                }

                var foodOrder = new FoodOrder()
                {
                    FoodId = food.Id,
                    OrderId = orderId
                };

                food.Quantity--;
                food.Orders.Add(foodOrder);
                order.Foods.Add(foodOrder);
            }

            this.data.SaveChanges();
        }

        private Food CreateFood(string name, double weight, decimal price, double profit, int quantity, DateTime expiration, int brandId, int categoryId)
        {
            return new Food()
            {
                Name = name,
                Weight = weight,
                DistributorPrice = price,
                Price = price + (price * (decimal)profit),
                Quantity = quantity,
                ExpirationDate = expiration,
                BrandId = brandId,
                CategoryId = categoryId
            };
        }
    }
}