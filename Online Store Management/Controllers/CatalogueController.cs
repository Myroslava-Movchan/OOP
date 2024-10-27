using Microsoft.AspNetCore.Mvc;

namespace Catalogue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly CatalogueClient _catalogueClient;

        public ProductController(CatalogueClient catalogueClient)
        {
            _catalogueClient = catalogueClient;
        }

        [HttpGet("images")]
        public async Task<IActionResult> GetProductImagesAsync(CancellationToken cancellationToken)
        {
            var images = await _catalogueClient.GetProductImagesAsync(cancellationToken);

            var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Product_Images");
            if (!Directory.Exists(imagesDirectory))
            {
                Directory.CreateDirectory(imagesDirectory);
            }

            foreach (var image in images)
            {
                var filePath = Path.Combine(imagesDirectory, $"products-{image.Name}");

                await System.IO.File.WriteAllBytesAsync(filePath, image.Content, cancellationToken);
            }

            return Ok(new { Message = "Product images downloaded successfully.", Directory = imagesDirectory });
        }
    }
}
