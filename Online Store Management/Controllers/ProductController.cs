using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Models;
using Online_Store_Management.Services;

namespace Online_Store_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService productService;
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

        public ProductController() 
        { 
            productService = new ProductService();
        }

        [HttpGet]
        public Product? GetProduct()
        {
            var product = productService.GetProduct();
            return product;
        }
    }
}
