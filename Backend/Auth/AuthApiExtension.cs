using Backend.Auth.DTOs;
using Backend.Auth.Services.Interfaces;
using Backend.EF.Models;

namespace Backend.Auth
{
    public static class AuthApiExtension
    {
        private static void MapLogin(this RouteGroupBuilder builder)
        {
            builder.MapPost("/login", (LoginDTO data, IUserService userService, ITokenService tokenService) =>
            {
                // находим пользователя
                User? person = userService.GetPersonWithPassword(data);
                // если пользователь не найден, отправляем статусный код 401
                if (person is null) return Results.Unauthorized();

                // создаем токен
                var token = tokenService.GenerateToken(person.Email, person.Role);

                return Results.Ok(token);
            })
                .Produces<string>()
                .WithName("auth login")
                .WithDescription("login endpoint");
        }

        private static void MapRegister(this RouteGroupBuilder builder)
        {
            builder.MapPost("/register", (LoginDTO data, IUserService userService) =>
            {
                bool created = userService.CreateUser(data);

                if (!created) return Results.Json<string>(statusCode: StatusCodes.Status400BadRequest, data: $"User {data.Email} not created, already exists");

                return Results.Json<string>(statusCode: StatusCodes.Status201Created, data: $"User {data.Email} created!");
            })
                .Produces(statusCode: StatusCodes.Status201Created)
                .WithName("auth register")
                .WithDescription("register endpoint");
        }

        private static void MapPasswordChange(this RouteGroupBuilder builder)
        {
            builder.MapPost("/changepassword", (PasswordResetDTO data, IUserService userService) =>
            {
                bool changed = userService.ChangePassword(data);

                if (!changed) return Results.Json<string>(statusCode: StatusCodes.Status400BadRequest, data: $"Password not changed, bad login data");

                return Results.Ok($"Password changed!");
            })
                .WithName("auth change password")
                .WithDescription("password change endpoint");
        }

        public static RouteGroupBuilder MapAuthEndpoints(this WebApplication app, string authRouteBase = "/api/auth")
        {
            var builder = app.MapGroup(authRouteBase);

            builder.MapLogin();
            builder.MapRegister();
            builder.MapPasswordChange();

            builder
                .WithTags("auth")
                .WithOpenApi();

            return builder;
        }
    }
}