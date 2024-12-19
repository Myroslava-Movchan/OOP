using Online_Store_Management.Interfaces;

using Online_Store_Management.Models;

namespace Online_Store_Management.DataAccess
{
    public class UserRepository : IUserRepository
    {
        readonly Dictionary<string, string> users = new()
        {
            { "john.doe@gmail.com", "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f" },
            { "jane.doe@gmail.com", "4ebd3d5f714ffedf6d9375713a76f44d4429461c1d89e214e005fac689f6e881" },
            { "developer@gmail.com", "d64303a579dd4ef07d5da37191991c26cdc4d8db41aa238633d5696af464a4c8" },
            { "manager@gmail.com", "632321774977f370359d966cd45112e46d2389a501a663de1c44ab1486d8d9a1" },
            { "support@gmail.com", "a474dc6337f18cb32256fd7fe43c14c3040fe5e4d1038dae323ceb2230ccddcb" },
            { "guest@gmail.com", "674c4c7106319650affbf1b8e75a507acc405bbaeb4d56ae982ab0a68b2cce8c" },
            { "tester@gmail.com", "e1a4b0c5fccb7980f81a0c6808093b68cd600298a58e8b09b58fc88838beace5" },
        };

        public User GetUserByEmail(string email)
        {
            users.TryGetValue(email, out string? passwordHash);

            return new User
            {
                Email = email,
                PasswordHash = passwordHash ?? throw new InvalidOperationException("Login or password is invalid")
            };
        }
    }
}
