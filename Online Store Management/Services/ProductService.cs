using Online_Store_Management.Models;
using Online_Store_Management.Infrastructure;
using Online_Store_Management.Interfaces;
namespace Online_Store_Management.Services
{
    public class ProductService : IProduct
    {
        private static readonly string[] Products = new[]
        {
            "T-Shirt", "Jeans",
            "Sweater", "Jacket",
            "Dress", "Skirt",
            "Shorts", "Blouse",
            "Suit", "Coat",
            "Hoodie", "Pajamas",
            "Belt", "Scarf",
            "Hat", "Socks",
            "Boots", "Sneakers"
        };
        private readonly Logger logger;
        public ProductService(Logger logger)
        {
            this.logger = logger;
        }

        public Product GetProduct()
        {
            var productName = Products[Random.Shared.Next(Products.Length)];
            var product = new Product()
            {
                ProductId = Random.Shared.Next(1, 18),
                ProductName = productName,
                ProductPrice = Random.Shared.Next(8, 230),
                ProductQuantity = Random.Shared.Next(1, 10)
            };

            return product;
        }

        public ProductStruct GetProductStruct()
        {
            var productName = Products[Random.Shared.Next(Products.Length)];
            var productStruct = new ProductStruct(
                productId: Random.Shared.Next(1, 18),
                productName: productName,
                productPrice: Random.Shared.Next(8, 230),
                productQuantity: Random.Shared.Next(1, 10)
            );

            return productStruct;
        }

    }
}
