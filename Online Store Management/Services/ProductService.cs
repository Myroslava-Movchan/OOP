using Online_Store_Management.Models;
namespace Online_Store_Management.Services
{
    public class ProductService
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

        public Product GetProduct()
        {
            var productName = Products[Random.Shared.Next(Products.Length)];
            var product = new Product()
            {
                ProductId = Random.Shared.Next(1, 18),
                ProductName = productName,
                ProductPrice = Random.Shared.Next(8, 230)
            };

            return product;
        }
    }
}
