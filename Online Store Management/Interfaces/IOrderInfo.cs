using Online_Store_Management.Models;

namespace Online_Store_Management.Interfaces
{
    public interface IOrderInfo
    {
        OrderInfo Post(Product product);
        bool CompareOrders(OrderInfo order);
        bool AddToTable(OrderInfo order);
        int EstimateDelivery();
        int EstimateDelivery(Product product);
    }
}
