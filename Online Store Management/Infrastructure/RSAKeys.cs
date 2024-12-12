using Microsoft.Extensions.Configuration;

public class RsaKeys
{
    private readonly IConfiguration _configuration;
    public RsaKeys(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetPrivateKey()
    {
        return _configuration["Encryption:PrivateKey"];
    }
}
