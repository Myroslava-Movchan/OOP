namespace Catalogue
{
    public class CatalogueClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ProductsUrl = "https://fakestoreapi.com/products";

        public CatalogueClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<(string? Name, byte[] Content)>> GetProductImagesAsync(CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(ProductsUrl, cancellationToken);

            response.EnsureSuccessStatusCode();

            var products = response.Content.ReadFromJsonAsAsyncEnumerable<ExternalProduct>(cancellationToken);
            var results = new List<(string? Name, byte[] Content)>();

            await foreach (var product in products.WithCancellation(cancellationToken))
            {
                if (product?.Image == null) continue;

                var imageName = Path.GetFileName(product.Image);
                var imageContent = await FetchImageContentAsync(httpClient, product.Image, cancellationToken);

                if (imageContent != null)
                {
                    results.Add((imageName, imageContent));
                }
            }

            return results;
        }

        private async Task<byte[]?> FetchImageContentAsync(HttpClient httpClient, string imageUrl, CancellationToken cancellationToken)
        {
            var imageResponse = await httpClient.GetAsync(imageUrl, cancellationToken);
            if (!imageResponse.IsSuccessStatusCode) return null;

            using var imageStream = await imageResponse.Content.ReadAsStreamAsync(cancellationToken);
            using var ms = new MemoryStream();
            await imageStream.CopyToAsync(ms, cancellationToken);
            return ms.ToArray();
        }
    }

    public class ExternalProduct
    {
        public int Id { get; set; }
        public string? Tittle { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
        public ProductRating? Rating { get; set; }
    }

    public class ProductRating
    {
        public int Count { get; set; }
        public decimal Rate { get; set; }
    }
}
