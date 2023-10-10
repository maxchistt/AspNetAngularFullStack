using Backend.Auth.DTOs;
using Backend.Auth.Services.Interfaces;

namespace Backend.Auth
{
    public static class AuthEndpointHandlers
    {
        public static IResult Login(LoginDTO data, IAuthService authService)
        {
            // пробуем получить токен
            string? token = authService.Login(data);
            // если не удалось, отправляем статусный код 401
            if (token is null) return Results.Unauthorized();
            // возвращаем токен
            return Results.Ok(token);
        }

        public static IResult Register(LoginDTO data, IAuthService authService)
        {
            bool created = authService.Register(data);

            if (!created) return Results.BadRequest($"User {data.Email} not created, already exists");

            return Results.Json(statusCode: StatusCodes.Status201Created, data: $"User {data.Email} created!");
        }

        public static IResult PasswordChange(PasswordResetDTO data, IAuthService authService)
        {
            bool changed = authService.PasswordChange(data);

            if (!changed) return Results.BadRequest($"Password not changed, bad login data");

            return Results.Ok($"Password changed!");
        }
    }
}