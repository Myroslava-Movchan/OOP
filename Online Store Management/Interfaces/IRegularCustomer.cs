using Online_Store_Management.Models;

namespace Online_Store_Management.Services
{
    public interface IRegularCustomer
    {
        Task<RegularCustomer> GetRegularCustomerAsync(CancellationToken cancellationToken);
    }
}
