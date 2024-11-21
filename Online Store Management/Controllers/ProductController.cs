using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Models;
using Online_Store_Management.Services;

namespace Online_Store_Management.Controllers
{
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
    }
}
