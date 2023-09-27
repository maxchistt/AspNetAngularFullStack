using Backend.Auth.DTOs;
using Backend.Auth.Params;
using Backend.EF.Models;

namespace Backend.Auth.Services.Interfaces
{
    public interface IUserService
    {
        public bool UserExists(string email);

        public User? GetPersonWithPassword(LoginDTO data);

        public bool CreateUser(LoginDTO loginData, Roles.Enum role = Roles.Enum.Client);

        public bool ChangePassword(PasswordResetDTO data);
    }
}