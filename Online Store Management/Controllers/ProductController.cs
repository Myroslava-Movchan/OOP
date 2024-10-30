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
        public async Task<Product?> GetProductAsync(CancellationToken cancellationToken)
        {
            var product = await productService.GetProductAsync(cancellationToken);
            return product;
        }

        [HttpGet("struct-product")]
        public async Task<ProductStruct> GetStructProductAsync(CancellationToken cancellationToken)
        {
            var structProduct = await productService.GetProductStructAsync(cancellationToken);
            return structProduct;
        }
    }
}
