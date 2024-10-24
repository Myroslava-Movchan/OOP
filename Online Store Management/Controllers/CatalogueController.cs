using Microsoft.AspNetCore.Mvc;

// this part (external API) was written with help of ChatGPT 
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

        [HttpGet("socks")]
        public async Task<IActionResult> GetSocksImagesAsync(CancellationToken cancellationToken)
        {

            var images = _catalogueClient.GetSocksImageAsync(cancellationToken);

            var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "SocksImages");
            if (!Directory.Exists(imagesDirectory))
            {
                Directory.CreateDirectory(imagesDirectory);
            }

            await foreach (var image in images.WithCancellation(cancellationToken))
            {
                var filePath = Path.Combine(imagesDirectory, $"socks-{image.Name}");

                await System.IO.File.WriteAllBytesAsync(filePath, image.Content, cancellationToken);
            }

            return Ok(new { Message = "Socks images downloaded successfully.", Directory = imagesDirectory });
        }
    }
}
