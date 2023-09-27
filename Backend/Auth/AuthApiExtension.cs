using Backend.Auth.DTOs;
using Backend.Auth.Services.Interfaces;
using Backend.EF.Models;
using Backend.Shared;

namespace Backend.Auth
{
    public static class AuthApiExtension
    {
        private static void MapLogin(this RouteGroupBuilder builder)
        {
            builder.MapPost("/login", (HttpRequest request, IUserService userService, ITokenService tokenService) =>
            {
                // получаем из формы email и пароль
                var form = request.Form;
                // если email и/или пароль не установлены, посылаем статусный код ошибки 400
                if (!FormMapper.Validate<LoginDTO>(form))
                    return Results.BadRequest("Email и/или пароль не установлены");

                // парсим форму
                LoginDTO data = FormMapper.Map<LoginDTO>(form);

                // находим пользователя
                User? person = userService.GetPersonWithPassword(data);
                // если пользователь не найден, отправляем статусный код 401
                if (person is null) return Results.Unauthorized();

                // создаем токен
                var token = tokenService.GenerateToken(person.Email, person.Role);

                return Results.Ok(token);
            })
                .Accepts<LoginDTO>(contentType: HttpContentTypes.MultipatFormdata)
                .Produces<string>(statusCode: StatusCodes.Status200OK)
                .WithName("auth login")
                .WithDescription("login endpoint");
        }

        private static void MapRegister(this RouteGroupBuilder builder)
        {
            builder.MapPost("/register", (HttpRequest request, IUserService userService) =>
            {
                // получаем из формы email и пароль
                var form = request.Form;
                // если email и/или пароль не установлены, посылаем статусный код ошибки 400
                if (!FormMapper.Validate<LoginDTO>(form))
                    return Results.BadRequest("Email и/или пароль не установлены");

                // парсим форму
                LoginDTO data = FormMapper.Map<LoginDTO>(form);

                bool created = userService.CreateUser(data);

                if (!created) return Results.Json<string>(statusCode: StatusCodes.Status400BadRequest, data: $"User {data.Email} not created, already exists");

                return Results.Json<string>(statusCode: StatusCodes.Status201Created, data: $"User {data.Email} created!");
            })
                .Accepts<LoginDTO>(contentType: HttpContentTypes.MultipatFormdata)
                .Produces<string>(statusCode: StatusCodes.Status201Created)
                .WithName("auth register")
                .WithDescription("register endpoint");
        }

        private static void MapPasswordChange(this RouteGroupBuilder builder)
        {
            builder.MapPost("/changepassword", (HttpRequest request, IUserService userService) =>
            {
                // получаем из формы email и пароль
                var form = request.Form;
                // если email и/или пароль не установлены, посылаем статусный код ошибки 400
                if (!FormMapper.Validate<PasswordResetDTO>(form))
                    return Results.BadRequest("Email и/или пароль не установлены");

                // парсим форму
                PasswordResetDTO data = FormMapper.Map<PasswordResetDTO>(form);

                bool changed = userService.ChangePassword(data);

                if (!changed) return Results.Json<string>(statusCode: StatusCodes.Status400BadRequest, data: $"Password not changed, bad login data");

                return Results.Json<string>(statusCode: StatusCodes.Status200OK, data: $"Password changed!");
            })
                .Accepts<PasswordResetDTO>(contentType: HttpContentTypes.MultipatFormdata)
                .Produces<string>(statusCode: StatusCodes.Status200OK)
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