using Backend.Auth.DTOs;
using Backend.Auth.Services.Interfaces;
using Backend.EF.Models;

namespace Backend.Auth
{
    public static class AuthEndpointHandlers
    {
        public static IResult Login(LoginDTO data, IUserService userService, ITokenService tokenService)
        {
            // находим пользователя
            User? person = userService.GetPersonWithPassword(data);
            // если пользователь не найден, отправляем статусный код 401
            if (person is null) return Results.Unauthorized();

            // создаем токен
            var token = tokenService.GenerateToken(person.Email, person.Role);

            return Results.Ok(token);
        }

        public static IResult Register(LoginDTO data, IUserService userService)
        {
            bool created = userService.CreateUser(data);

            if (!created) return Results.Json(statusCode: StatusCodes.Status400BadRequest, data: $"User {data.Email} not created, already exists");

            return Results.Json(statusCode: StatusCodes.Status201Created, data: $"User {data.Email} created!");
        }

        public static IResult PasswordChange(LoginDTO data, IUserService userService)
        {
            bool created = userService.CreateUser(data);

            if (!created) return Results.Json(statusCode: StatusCodes.Status400BadRequest, data: $"User {data.Email} not created, already exists");

            return Results.Json(statusCode: StatusCodes.Status201Created, data: $"User {data.Email} created!");
        }
    }
}