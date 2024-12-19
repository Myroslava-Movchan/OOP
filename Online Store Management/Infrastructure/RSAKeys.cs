using Microsoft.Extensions.Configuration;

namespace Online_Store_Management.Infrastructure
{
    public class RsaKeys(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public string GetPrivateKey()
        {
            return _configuration["Encryption:PrivateKey"] ?? "Unknown key";
        }
    }
}