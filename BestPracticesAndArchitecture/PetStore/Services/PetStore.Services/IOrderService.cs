namespace PetStore.Services
{
    public interface IOrderService
    {
        int CreateOrder(int userId);

        void CompleteOrder(int orderId);

        void CancelOrder(int orderId);
    }
}