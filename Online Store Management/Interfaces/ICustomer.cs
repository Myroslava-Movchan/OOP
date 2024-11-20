using Online_Store_Management.DataAccess;
using Online_Store_Management.Models;
using static Online_Store_Management.Services.CustomerService;

namespace Online_Store_Management.Interfaces
{
    public interface ICustomer
    {
        Task LogActionAsync(Customer customer, string message, CancellationToken cancellationToken);
        void SetCustomerLogFileStream(FileStream customerLogFileStream);
        Task<CustomerDbModel> GetCustomerByIdAsync(int id, CancellationToken cancellationToken);
        Task AddCustomerAsync(CustomerDbModel customer, CancellationToken cancellationToken);
        Task UpdateAsync(CustomerDbModel customer, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        event CustomerUpdateHandler? CustomerUpdate;
    }
}
