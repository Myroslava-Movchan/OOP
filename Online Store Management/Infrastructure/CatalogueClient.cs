using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

// this part (external API) was written with help of ChatGPT 
namespace Catalogue
{
    public class CatalogueClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string SocksCategoryUrl = "https://maikbook.com/?product_cat=socks";

        public CatalogueClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async IAsyncEnumerable<(string? Name, byte[] Content)> GetSocksImageAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(SocksCategoryUrl, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch content from {SocksCategoryUrl}. Status Code: {response.StatusCode}");
            }

            var htmlContent = await response.Content.ReadAsStringAsync(cancellationToken);
            var imageUrls = ExtractImageUrlsFromHtml(htmlContent);

            foreach (var imageUrl in imageUrls)
            {
                var imageName = Path.GetFileName(imageUrl);
                byte[] content;

                var imageResponse = await httpClient.GetAsync(imageUrl, cancellationToken);
                if (!imageResponse.IsSuccessStatusCode)
                {
                    continue;
                }

                using (var imageStream = await imageResponse.Content.ReadAsStreamAsync(cancellationToken))
                {
                    using (var ms = new MemoryStream())
                    {
                        await imageStream.CopyToAsync(ms, cancellationToken);
                        content = ms.ToArray();
                    }
                }

                yield return (imageName, content);
            }
        }

        private IEnumerable<string> ExtractImageUrlsFromHtml(string htmlContent)
        {
            // Regex to match image URLs with extensions typically used for product images
            var regex = new Regex(@"<img[^>]+src=""([^""]+\.jpg|\.jpeg|\.png)""", RegexOptions.IgnoreCase);
            var matches = regex.Matches(htmlContent);

            foreach (Match match in matches)
            {
                if (match.Success && match.Groups.Count > 1)
                {
                    var url = match.Groups[1].Value;
                    if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    {
                        yield return url;
                    }
                }
            }
        }
    }
}
