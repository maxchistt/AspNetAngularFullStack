using Backend.Auth.DTOs;
using Backend.Auth.Models;

namespace Backend.Auth.Services
{
    public interface IUserService
    {
        public User? FindPerson(LoginDTO data);
    }
}