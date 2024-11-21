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
        Task<List<Product>> SearchProductCategory(string category, CancellationToken cancellationToken);
        Task<List<Product>> SearchProductAvailability(CancellationToken cancellationToken);
        Task<List<Product>> SearchProductPriceRange(int min, int max, CancellationToken cancellationToken);
        Task<List<Product>> GetBestProductsRating(CancellationToken cancellationToken);
    }
}
