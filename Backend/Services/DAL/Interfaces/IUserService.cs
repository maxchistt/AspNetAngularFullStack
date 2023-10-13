using Backend.Models.Users;
using Backend.Shared.AuthParams;

namespace Backend.Services.DAL.Interfaces
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