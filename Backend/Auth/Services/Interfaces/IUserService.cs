using Backend.Auth.DTOs;
using Backend.Auth.Models;
using Backend.Auth.Params;

namespace Backend.Auth.Services.Interfaces
{
    public interface IUserService
    {
        public bool UserExists(string email);

        public User? FindPersonWithPassword(LoginDTO data);

        public bool CreateUser(LoginDTO loginData, Roles.Enum? role = null);
    }
}