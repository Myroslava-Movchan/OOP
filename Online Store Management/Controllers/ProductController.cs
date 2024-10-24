using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Models;
using Online_Store_Management.Services;
using Online_Store_Management.Infrastructure;

namespace Online_Store_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService productService;
        private readonly Logger logger;


        public ProductController(Logger logger)
        {
            this.logger = logger;
            productService = new ProductService(logger);
        }

        [HttpGet]
        public Product? GetProduct()
        {
            var product = productService.GetProduct();
            return product;
        }

        [HttpGet("struct-product")]
        public ProductStruct GetStructProduct()
        {
            var structProduct = productService.GetProductStruct();
            return structProduct;
        }
    }
}
