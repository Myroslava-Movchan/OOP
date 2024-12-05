using Online_Store_Management.Models;

namespace Online_Store_Management.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
    }
}
