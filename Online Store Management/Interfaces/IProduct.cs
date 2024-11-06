using Online_Store_Management.Models;

namespace Online_Store_Management.Interfaces
{
    public interface IProduct
    {
        Task<Product> GetProductAsync(CancellationToken cancellationToken);
    }
}
