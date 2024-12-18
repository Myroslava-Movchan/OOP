using Online_Store_Management.Models;
using Online_Store_Management.Interfaces;
using System.Threading;
using Online_Store_Management.Infrastructure;
namespace Online_Store_Management.Services
{
    public class ProductService(IRepository<Product> productRepository) : IProduct
    {
        private readonly IRepository<Product> productRepository = productRepository
            ?? throw new ArgumentNullException(nameof(productRepository));
        private static readonly string[] Products =
        [
            "T-Shirt", "Jeans",
            "Sweater", "Jacket",
            "Dress", "Skirt",
            "Shorts", "Blouse",
            "Suit", "Coat",
            "Hoodie", "Pajamas",
            "Belt", "Scarf",
            "Hat", "Socks",
            "Boots", "Sneakers"
        ];
        private static readonly Dictionary<string, string> Categories = new()
        {
            { "T-Shirt", "Shirts" },
            { "Blouse", "Shirts" },
            { "Jeans", "Pants" },
            { "Sweater", "Shirts" },
            { "Jacket", "Outerwear" },
            { "Dress", "Full body" },
            { "Skirt", "Full body" },
            { "Shorts", "Pants" },
            { "Suit", "Full body" },
            { "Coat", "Outerwear" },
            { "Hoodie", "Shirts" },
            { "Pajamas", "Full body" },
            { "Belt", "Accessories" },
            { "Scarf", "Accessories" },
            { "Hat", "Accessories" },
            { "Socks", "Accessories" },
            { "Boots", "Shoes" },
            { "Sneakers", "Shoes" }
        };

        public async Task<Product> GetProductAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(50, cancellationToken);
            var productName = Products[Random.Shared.Next(Products.Length)];
            var category = Categories[productName];
            bool available = Random.Shared.Next(2) == 0;
            var product = new Product(
                productId: Random.Shared.Next(1, 18),
                productName: productName,
                productPrice: Random.Shared.Next(8, 230),
                category: category,
                available: available,
                rating: Random.Shared.Next(0, 5)

            );

            return product;
        }

        public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await productRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            var publicKey = @"-----BEGIN PUBLIC KEY-----
                            MIGeMA0GCSqGSIb3DQEBAQUAA4GMADCBiAKBgHITrVsjso0VgEjImMl+NNhVn+WCTgvWeCQ1j3O73F0v3AW8hIJwetiGAzYy2rY3Cu+TQDT0BthG67tuPFmUok0nEuZ/xBmrG6XHwTOsleH5z5V3CRoWzLU7iYHI8Bik7faWxJ/aJtfj149G0dYi3zdRWON4ptt3NuKnJvPRB15JAgMBAAE=
                            -----END PUBLIC KEY-----";
            var productInfo = $"{product.ProductId},{product.ProductName},{product.ProductPrice},{product.Category},{product.Availability},{product.Rating}";
            var encryptedData = RsaEncryption.Encrypt(productInfo, publicKey);

            await File.AppendAllTextAsync("EncryptedProducts.log", encryptedData + Environment.NewLine, cancellationToken);

            await productRepository.AddAsync(product, cancellationToken);
        }
        public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            await productRepository.UpdateAsync(product, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await productRepository.DeleteAsync(id, cancellationToken);
        }

        public IEnumerable<string> GetListOfProducts()
        {
            return Products.Where(p => p.Length > 4).ToList();
        }

        public async Task<List<Product>> SearchProductCategory(string category, CancellationToken cancellationToken)
        {
            var allProducts = await productRepository.GetAllAsync(cancellationToken);
            var filteredProducts = new List<Product>();
            foreach (var product in allProducts)
            {
                if (category == product.Category)
                {
                    filteredProducts.Add(product);
                }
            }
            return filteredProducts;
        }

        public async Task<List<Product>> SearchProductAvailability(CancellationToken cancellationToken)
        {
            var allProducts = await productRepository.GetAllAsync(cancellationToken);
            var filteredProducts = new List<Product>();
            foreach (var product in allProducts)
            {
                if (product.Availability == true)
                {
                    filteredProducts.Add(product);
                }
            }
            return filteredProducts;
        }

        public async Task<List<Product>> SearchProductPriceRange(int min, int max, CancellationToken cancellationToken)
        {
            var allProducts = await productRepository.GetAllAsync(cancellationToken);
            var filteredProducts = new List<Product>();
            foreach (var product in allProducts)
            {
                if (product.ProductPrice <= max && product.ProductPrice >= min)
                {
                    filteredProducts.Add(product);
                }
            }
            return filteredProducts;
        }

        public async Task<List<Product>> GetBestProductsRating(CancellationToken cancellationToken)
        {
            var allProducts = await productRepository.GetAllAsync(cancellationToken);
            var filteredProducts = new List<Product>();
            foreach (var product in allProducts)
            {
                if (product.Rating >= 4)
                {
                    filteredProducts.Add(product);
                }
            }
            return filteredProducts;
        }
    }
}
