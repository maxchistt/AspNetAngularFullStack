using Backend.DTOs.Auth;
using Backend.Services.BLL.Interfaces;

namespace Backend.Endpoints.Auth
{
    public static class AuthApi
    {
        public static RouteGroupBuilder MapAuthEndpoints(this WebApplication app, string authRouteBase = "/api/auth")
        {
            var builder = app.MapGroup(authRouteBase);

            // Login
            builder.MapPost("/login", (LoginDTO data, IAuthService authService) =>
            {
                string? token = authService.Login(data); // пробуем получить токен
                if (token is null) return Results.Unauthorized(); // если не удалось, отправляем статусный код 401
                return Results.Ok(token); // возвращаем токен
            })
               .Produces<string>()
               .WithName("auth login")
               .WithDescription("login endpoint");

            // Register
            builder.MapPost("/register", (LoginDTO data, IAuthService authService) =>
            {
                bool created = authService.Register(data);
                if (!created) return Results.BadRequest($"User {data.Email} not created, already exists");
                return Results.Json(statusCode: StatusCodes.Status201Created, data: $"User {data.Email} created!");
            })
                .Produces(statusCode: StatusCodes.Status201Created)
                .WithName("auth register")
                .WithDescription("register endpoint");

            // Change password
            builder.MapPost("/changepassword", (PasswordResetDTO data, IAuthService authService) =>
            {
                bool changed = authService.PasswordChange(data);
                if (!changed) return Results.BadRequest($"Password not changed, bad login data");
                return Results.Ok($"Password changed!");
            })
                .WithName("auth change password")
                .WithDescription("password change endpoint");

            builder
                .WithTags("auth")
                .WithOpenApi();

            return builder;
        }
    }
}