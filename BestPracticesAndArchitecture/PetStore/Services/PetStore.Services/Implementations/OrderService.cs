namespace PetStore.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PetStore.Data;
    using PetStore.Data.Models;

    public class OrderService : IOrderService
    {
        private readonly PetStoreDbContext data;

        public OrderService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public void CancelOrder(int orderId)
        {
            var order = this.data.Orders.Find(orderId);

            if (order == null)
            {
                throw new ArgumentNullException(String.Format(OutputMessages.OrderNotExists, orderId));
            }

            order.Status = OrderStatus.Completed;
            this.data.SaveChanges();
        }

        public void CompleteOrder(int orderId)
        {
            var order = this.data.Orders.Find(orderId);

            if (order == null)
            {
                throw new ArgumentNullException(String.Format(OutputMessages.OrderNotExists, orderId));
            }

            order.Status = OrderStatus.Cancelled;
            this.data.SaveChanges();
        }

        public int CreateOrder(int userId)
        {
            if (this.data.Users.Any(u=>u.Id == userId) == false)
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