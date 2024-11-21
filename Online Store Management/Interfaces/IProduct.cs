using Online_Store_Management.Models;

namespace Online_Store_Management.Interfaces
{
    public interface IProduct
    {
        Task<Product> GetProductAsync(CancellationToken cancellationToken);
        IEnumerable<string> GetListOfProducts();
        Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
        Task AddProductAsync(Product product, CancellationToken cancellationToken);
        Task UpdateAsync(Product product, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
