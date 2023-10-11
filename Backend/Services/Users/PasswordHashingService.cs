using Backend.Services.Users.Interfaces;

namespace Backend.Services.Users
{
    public class PasswordHashingService : IPasswordHashingService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 12);
        }

        public bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}