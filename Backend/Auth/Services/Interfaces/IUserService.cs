using Backend.Auth.DTOs;
using Backend.Auth.Models;
using Backend.Auth.Params;

namespace Backend.Auth.Services.Interfaces
{
    public interface IUserService
    {
        public bool UserExists(string email);

        public User? GetPersonWithPassword(LoginDTO data);

        public bool CreateUser(LoginDTO loginData, Roles.Enum? role = null);

        public bool ChangePassword(PasswordResetDTO data);
    }
}