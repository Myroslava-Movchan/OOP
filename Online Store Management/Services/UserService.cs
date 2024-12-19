using Online_Store_Management.Interfaces;
using Online_Store_Management.Models;
using System.Security.Cryptography;
using System.Text;

namespace Online_Store_Management.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public User? GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public bool VerifyHashedPassword(string plainPassword, string hashPassword)
        {
            return hashPassword == HashPassword(plainPassword);
        }

        private static string HashPassword(string plainPassword)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
