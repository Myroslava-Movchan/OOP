using System.Security.Claims;

namespace Online_Store_Management.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string username);

        ClaimsPrincipal ValidateToken(string token);
    }
}
