using Online_Store_Management.Models;

namespace Online_Store_Management.Interfaces
{
    public interface ICustomer
    {
        void LogAction(string message);
        void SetCustomerLogFileStream(FileStream customerLogFileStream);
        Task<Discount> GetNewCustomerAsync(CancellationToken cancellationToken);
        Task<Discount> GetRegularCustomerAsync(CancellationToken cancellationToken);
        Task<Customer> GetCustomerAsync(int id, CancellationToken cancellationToken);
    }
}
