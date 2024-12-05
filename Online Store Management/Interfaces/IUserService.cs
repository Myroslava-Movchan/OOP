using Online_Store_Management.Models;

namespace Online_Store_Management.Interfaces
{
    public interface IUserService
    {
        User? GetUserByEmail(string email);

        bool VerifyHashedPassword(string plainPassword, string hashPassword);
    }
}
