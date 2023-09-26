using Backend.Auth.DTOs;
using Backend.Auth.Models;
using Backend.Auth.Params;
using Backend.Auth.Services.Interfaces;
using Backend.EF;

namespace Backend.Auth.Services
{
    public class UserService : IUserService
    {
        private DataContext Context { get; }
        private IPasswordHashingService Hasher { get; }

        public UserService(DataContext dataContext, IPasswordHashingService hasher)
        {
            Context = dataContext;
            Hasher = hasher;
        }

        public bool CreateUser(LoginDTO loginData, Roles.Enum? role = null)
        {
            if (UserExists(loginData.Email)) return false;

            Context.Users.Add(new User(
                loginData.Email,
                Hasher.HashPassword(loginData.Password),
                Roles.GetRoleByEnum(role ?? Roles.Enum.Client)
            ));

            Context.SaveChanges();

            return true;
        }

        public User? FindPersonWithPassword(LoginDTO data)
        {
            var user = Context.Users.FirstOrDefault(p => p.Email == data.Email);
            if (user is null) return null;

            bool passwordVerified = Hasher.Verify(data.Password, user.Password);
            if (!passwordVerified) return null;

            return user;
        }

        public bool UserExists(string email)
        {
            return Context.Users.Any(u => u.Email == email);
        }
    }
}