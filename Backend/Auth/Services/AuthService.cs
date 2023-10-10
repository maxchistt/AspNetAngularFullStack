using Backend.Auth.DTOs;
using Backend.Auth.Services.Interfaces;
using Backend.EF.Models;

namespace Backend.Auth.Services
{
    public class AuthService : IAuthService
    {
        private IUserService userService;
        private ITokenService tokenService;

        public AuthService(IUserService userService, ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        public string? Login(LoginDTO data)
        {
            // находим пользователя
            User? person = userService.GetPersonWithPassword(data);

            // если пользователь не найден, отправляем null
            if (person is null) return null;

            // создаем токен
            var token = tokenService.GenerateToken(person.Email, person.Role);
            return token;
        }

        public bool Register(LoginDTO data)
        {
            bool created = userService.CreateUser(data);
            return created;
        }

        public bool PasswordChange(PasswordResetDTO data)
        {
            bool changed = userService.ChangePassword(data);

            return changed;
        }
    }
}