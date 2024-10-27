using Online_Store_Management.Models;

namespace Online_Store_Management.Interfaces
{
    public interface ICustomer
    {
        void LogAction(string message);
        void SetCustomerLogFileStream(FileStream customerLogFileStream);
        Discount GetNewCustomer();
        Discount GetRegularCustomer();
        Task<Customer> GetCustomerAsync(int id, CancellationToken cancellationToken);
    }
}
