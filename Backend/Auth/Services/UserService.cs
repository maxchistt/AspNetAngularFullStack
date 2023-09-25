using Backend.Auth.DTOs;
using Backend.Auth.Models;
using Backend.Auth.Params;
using Backend.Auth.Services.Interfaces;
using Backend.EF;

namespace Backend.Auth.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext dataContext)
        {
            _context = dataContext;
        }

        public bool CreateUser(LoginDTO loginData, Roles.Enum? role = null)
        {
            if (UserExists(loginData.Email)) return false;

            _context.Users.Add(new User(
                loginData.Email,
                loginData.Password,
                Roles.GetRoleByEnum(role ?? Roles.Enum.Client)
            ));

            _context.SaveChanges();

            return true;
        }

        public User? FindPersonWithPassword(LoginDTO data)
        {
            return _context.Users.Where(p => p.Email == data.Email && p.Password == data.Password).First();
        }

        public bool UserExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}