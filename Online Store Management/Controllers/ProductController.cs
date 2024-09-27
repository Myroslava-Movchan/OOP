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
