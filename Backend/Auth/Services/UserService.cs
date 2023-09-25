using Backend.Auth.DTOs;
using Backend.Auth.Models;
using Backend.Auth.Params;
using Backend.Auth.Services.Interfaces;

namespace Backend.Auth.Services
{
    public class UserService : IUserService
    {
        public List<User> users = new List<User>
        {
            new User("tom@gmail.com", "12345", Roles.Admin),
            new User("bob@gmail.com", "55555", Roles.Client),
        };

        public bool CreateUser(LoginDTO loginData, Roles.Enum? role = null)
        {
            if (UserExists(loginData.Email)) return false;

            users.Add(new User(
                loginData.Email,
                loginData.Password,
                Roles.GetRoleByEnum(role ?? Roles.Enum.Client)
            ));

            return true;
        }

        public User? FindPersonWithPassword(LoginDTO data)
        {
            return users.Find(p => p.Email == data.Email && p.Password == data.Password);
        }

        public bool UserExists(string email)
        {
            return users.Any(u => u.Email == email);
        }
    }
}