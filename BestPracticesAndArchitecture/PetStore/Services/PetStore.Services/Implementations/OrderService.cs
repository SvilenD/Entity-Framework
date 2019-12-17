namespace PetStore.Services.Implementations
{
    using System;

    using PetStore.Data;
    using PetStore.Data.Models;

    public class OrderService : IOrderService
    {
        private readonly PetStoreDbContext data;
        private readonly UserService userService;

        public OrderService(PetStoreDbContext data, UserService userService)
        {
            this.data = data;
            this.userService = userService;
        }

        public void CancelOrder(int orderId)
        {
            var order = this.data.Orders.Find(orderId);

            if (order == null)
            {
                throw new ArgumentNullException(String.Format(OutputMessages.OrderNotExists, orderId));
            }

            order.Status = OrderStatus.Cancelled;
            foreach (var food in order.Foods)
            {
                int quantity = food.Food.Quantity;
                var returnedFood = this.data.Foods.Find(food.FoodId);

                returnedFood.Quantity += quantity;
            }

            foreach (var toy in order.Toys)
            {
                int quantity = toy.Toy.Quantity;
                var returnedToy = this.data.Toys.Find(toy.ToyId);

                returnedToy.Quantity += quantity;
            }

            foreach (var pet in order.Pets)
            {
            }
            this.data.SaveChanges();
        }

        public void CompleteOrder(int orderId)
        {
            var order = this.data.Orders.Find(orderId);

            if (order == null)
            {
                throw new ArgumentNullException(String.Format(OutputMessages.OrderNotExists, orderId));
            }

            order.Status = OrderStatus.Completed;
            this.data.SaveChanges();
        }

        public int CreateOrder(int userId)
        {
            if (userService.Exists(userId) == false)
            {
                throw new InvalidOperationException(String.Format(OutputMessages.UserNotExists, userId));
            }

            var order = new Order()
            {
                PurchaseDate = DateTime.Now,
                Status = OrderStatus.Pending,
                UserId = userId
            };

            this.data.Orders.Add(order);
            this.data.SaveChanges();

            return order.Id;
        }
    }
}