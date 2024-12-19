using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Infrastructure;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Models;

namespace Online_Store_Management.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductController(IProduct productService, RsaKeys rsaKeys) : ControllerBase
    {
        private readonly IProduct productService = productService;
        private readonly RsaKeys rsaKeyProvider = rsaKeys;

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

        [HttpGet("Decrypt and Read Products")]
        public async Task<List<Product>> DecryptAndReadProducts(CancellationToken cancellationToken)
        {
            var privateKey = rsaKeyProvider.GetPrivateKey();
            var encryptedLines = await System.IO.File.ReadAllLinesAsync("EncryptedProducts.log", cancellationToken);

            var products = new List<Product>();
            foreach (var encryptedLine in encryptedLines)
            {
                var decryptedText = RsaEncryption.Decrypt(encryptedLine, privateKey);
                var parts = decryptedText.Split(',');
                products.Add(new Product
                {
                    ProductId = int.Parse(parts[0]),
                    ProductName = parts[1],
                    ProductPrice = decimal.Parse(parts[2]),
                    Category = parts[3],
                    Availability = bool.Parse(parts[4]),
                    Rating = int.Parse(parts[5])
                });
            }

            return products;
        }
    }
}
