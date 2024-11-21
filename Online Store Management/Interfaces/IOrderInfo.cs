using Online_Store_Management.Models;

namespace Online_Store_Management.Interfaces
{
    public interface IOrderInfo
    {
        Task<OrderInfo> PostAsync(Product product, CancellationToken cancellationToken);
        Task<bool> CompareOrdersAsync(OrderInfo order, CancellationToken cancellationToken);
        Task<bool> AddToTableAsync(OrderInfo order, CancellationToken cancellationToken);
        Task<int> EstimateDeliveryAsync(CancellationToken cancellationToken);
        decimal GetTotal(OrderInfo order);
        Task<OrderInfo?> GetOrderByIdAsync(int orderNumber, CancellationToken cancellationToken);
        Task AddOrderAsync(OrderInfo order, CancellationToken cancellationToken);
        Task UpdateAsync(OrderInfo order, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
