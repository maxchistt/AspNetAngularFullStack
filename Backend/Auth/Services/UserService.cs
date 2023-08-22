using Backend.Auth.DTOs;
using Backend.Auth.Models;
using Backend.Auth.Params;

namespace Backend.Auth.Services
{
    public class UserService : IUserService
    {
        public List<User> users = new List<User>
        {
            new User("tom@gmail.com", "12345", Roles.Admin),
            new User("bob@gmail.com", "55555", Roles.Client),
        };

        public User? FindPerson(LoginDTO data)
        {
            return users.Find(p => p.Email == data.Email && p.Password == data.Password);
        }
    }
}