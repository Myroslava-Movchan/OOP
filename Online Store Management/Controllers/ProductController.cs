using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Models;

namespace Online_Store_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
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

        [HttpGet]
        public Product? GetProduct()
        {
            var productName = Products[Random.Shared.Next(Products.Length)];
            var product = new Product() 
            {
                ProductId = Random.Shared.Next(1,18),
                ProductName = productName,
                ProductPrice = Random.Shared.Next(8, 230)
            };

            return product;

        }
    }
}
