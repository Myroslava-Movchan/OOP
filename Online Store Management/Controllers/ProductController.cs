using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Models;

namespace Online_Store_Management.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProduct productService;
        public ProductController(IProduct productService)
        {
            this.productService = productService;
        }

        [HttpGet("Create product")]
        public async Task<Product> GetProductAsync(CancellationToken cancellationToken)
        {
            var structProduct = await productService.GetProductAsync(cancellationToken);
            return structProduct;
        }

        [HttpPost("Add product")]
        public async Task AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            await productService.AddProductAsync(product, cancellationToken);
        } 

        [HttpPut("Update product")]
        public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            await productService.UpdateAsync(product, cancellationToken);
        }

        [HttpDelete("Delete product")]
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await productService.DeleteAsync(id, cancellationToken);
        }

        [HttpGet("Search products by category")]
        public async Task<List<Product>> SearchByCategory(string category, CancellationToken cancellationToken)
        {
            return await productService.SearchProductCategory(category, cancellationToken);
        }

        [HttpGet("Search products by availability")]
        public async Task<List<Product>> SearchByAvailability(CancellationToken cancellationToken)
        {
            return await productService.SearchProductAvailability(cancellationToken);
        }

        [HttpGet("Search products by price range")]
        public async Task<List<Product>> SearchByPriceRange(int min, int max, CancellationToken cancellationToken)
        {
            return await productService.SearchProductPriceRange(min, max, cancellationToken);
        }

        [HttpGet("Get list of products with the highest rating")]
        public async Task<List<Product>> GetBestRated(CancellationToken cancellationToken)
        {
            return await productService.GetBestProductsRating(cancellationToken);
        }
    }
}
