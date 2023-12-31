﻿using Backend.DTOs.Auth;
using Backend.Models.Users;
using Backend.Services.BLL.Interfaces;
using Backend.Services.DAL.Interfaces;
using Backend.Services.Utilities.BLL.Interfaces;

namespace Backend.Services.BLL
{
    public class AuthService : IAuthService
    {
        private IUsersService userService;
        private ITokenService tokenService;

        public AuthService(IUsersService userService, ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        public string? Login(LoginDTO data)
        {
            // находим пользователя
            User? person = userService.GetUserWithPasswordCheck(data.Email, data.Password);

            // если пользователь не найден, отправляем null
            if (person is null) return null;

            // создаем токен
            var token = tokenService.GenerateToken(person.Email, person.Role);
            return token;
        }

        public bool Register(LoginDTO data)
        {
            bool created = userService.CreateUser(data.Email, data.Password);
            return created;
        }

        public bool PasswordChange(PasswordResetDTO data)
        {
            var user = userService.GetUserWithPasswordCheck(data.Email, data.Password);

            if (user is null) return false;

            bool changed = userService.SetNewPassword(data.Email, data.NewPassword);

            return changed;
        }
    }
}