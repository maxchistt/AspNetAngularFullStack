using Backend.EF.Models;
using Backend.Shared.Auth.Params;

namespace Backend.Services.Users.Interfaces
{
    public interface IUserService
    {
        public bool UserExists(string email);

        public User? GetUser(string email);

        public User? GetUserWithPasswordCheck(string email, string password);

        public bool CreateUser(string email, string password, Roles.Enum role = Roles.Enum.Client);

        public bool SetNewPassword(string email, string newPassword);
    }
}