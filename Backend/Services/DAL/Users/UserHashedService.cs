using Backend.EF.Context;
using Backend.Models.Users;
using Backend.Services.DAL.Users.Interfaces;
using Backend.Shared.AuthParams;

namespace Backend.Services.DAL.Users
{
    public class UserHashedService : IUserService
    {
        private DataContext Context { get; }
        private IPasswordHashingService Hasher { get; }

        public UserHashedService(DataContext dataContext, IPasswordHashingService hasher)
        {
            Context = dataContext;
            Hasher = hasher;
        }

        public bool CreateUser(string email, string password, Roles.Enum role = Roles.Enum.Client)
        {
            if (UserExists(email)) return false;

            Context.Users.Add(new User(
                email,
                Hasher.HashPassword(password),
                Roles.GetRoleByEnum(role)
            ));

            Context.SaveChanges();

            return true;
        }

        public User? GetUser(string email)
        {
            return Context.Users.FirstOrDefault(p => p.Email == email);
        }

        public User? GetUserWithPasswordCheck(string email, string password)
        {
            var user = GetUser(email);
            if (user is null) return null;

            bool passwordVerified = Hasher.Verify(password, user.Password);
            if (!passwordVerified) return null;

            return user;
        }

        public bool UserExists(string email)
        {
            return Context.Users.Any(u => u.Email == email);
        }

        public bool SetNewPassword(string email, string newPassword)
        {
            var user = GetUser(email);
            if (user is null) return false;

            user.Password = Hasher.HashPassword(newPassword);

            Context.Update(user);
            Context.SaveChanges();

            return true;
        }
    }
}