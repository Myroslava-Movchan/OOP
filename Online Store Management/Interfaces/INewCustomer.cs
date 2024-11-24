using Online_Store_Management.Models;

namespace Online_Store_Management.Services
{
    public interface INewCustomer
    {
        Task<NewCustomer> GetNewCustomerAsync(CancellationToken cancellationToken);
    }
}
