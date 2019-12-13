namespace PetStore.Services
{
    using PetStore.Data.Models;
    using System.Collections.Generic;

    public interface IOrderService
    {
        int CreateOrder(int userId);

        void CompleteOrder(int orderId);

        void CancelOrder(int orderId);
    }
}