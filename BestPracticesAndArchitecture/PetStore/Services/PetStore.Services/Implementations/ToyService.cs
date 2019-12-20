namespace PetStore.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using Data.Models;
    using Services.Models.Toy;
    using static Validator;

    public class ToyService : IToyService
    {
        private readonly PetStoreDbContext data;

        public ToyService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public void BuyFromDistributor(string name, string description, decimal price, double profit, int quantity, int brandId, int categoryId)
        {
            if (this.data.Brands.Any(b => b.Id == brandId) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidBrand);
            }
            else if (this.data.Categories.Any(c => c.Id == categoryId) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidCategory);
            }

            var toy = CreateToy(name, description, price, profit, quantity, brandId, categoryId);

            if (IsValid(toy) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidToy);
            }

            this.data.Toys.Add(toy);
            this.data.SaveChanges();
        }

        public void BuyFromDistributor(AddingToyServiceModel model)
        {
            if (this.data.Brands.Any(b => b.Id == model.BrandId) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidBrand);
            }
            else if (this.data.Categories.Any(c => c.Id == model.CategoryId) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidCategory);
            }

            var toy = CreateToy(model.Name, model.Description, model.Price, model.Profit, model.Quantity, model.BrandId, model.CategoryId);

            if (IsValid(toy) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidToy);
            }

            this.data.Toys.Add(toy);
            this.data.SaveChanges();
        }

        public IEnumerable<ToyListingServiceModel> SearchByName(string name)
             => this.data.Toys
                .Where(t => t.Name.ToLower().Contains(name.ToLower()))
                .Select(t => new ToyListingServiceModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Price = t.Price,
                    Quantity = t.Quantity
                })
                .ToList();

        public void SellToysToUser(List<int> toyIds, int orderId)
        {
            var order = this.data.Orders.Find(orderId);
            if (order == null)
            {
                throw new ArgumentNullException(String.Format(OutputMessages.OrderNotExists, orderId));
            }

            foreach (var id in toyIds)
            {
                var toy = this.data.Toys.Find(id);

                if (toy == null || toy.Quantity < 1)
                {
                    throw new InvalidOperationException(String.Format(OutputMessages.ToyNotExists, id));
                }

                var toyOrder = new ToyOrder()
                {
                    ToyId = toy.Id,
                    OrderId = orderId
                };

                toy.Quantity--;
                this.data.ToyOrders.Add(toyOrder);
                order.Toys.Add(toyOrder);
            }

            this.data.SaveChanges();
        }

        private Toy CreateToy(string name, string description, decimal price, double profit, int quantity, int brandId, int categoryId)
            => new Toy()
            {
                Name = name,
                Description = description,
                DistributorPrice = price,
                Price = price + (price * (decimal)profit),
                Quantity = quantity,
                BrandId = brandId,
                CategoryId = categoryId
            };
    }
}