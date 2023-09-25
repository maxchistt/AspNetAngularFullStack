using Backend.Auth.DTOs;
using Backend.Auth.Models;
using Backend.Auth.Services;
using Backend.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Backend.Auth
{
    public static class AuthApiExtension
    {
        public static RouteGroupBuilder UseAuthAndMapEndpoints(this WebApplication app, string authRouteBase = "/api/auth")
        {
            app.UseAuth();
            return app.MapAuthEndpoints(authRouteBase);
        }

        public static void UseAuth(this WebApplication app)
        {
            app.UseStatusCodePages(context =>
            {
                var request = context.HttpContext.Request;
                var response = context.HttpContext.Response;

                if (response.StatusCode is StatusCodes.Status401Unauthorized or StatusCodes.Status403Forbidden)
                {
                    response.Redirect("/api/auth/accessdenied");
                }

                return Task.CompletedTask;
            });
            app.UseAuthentication();
            app.UseAuthorization();
        }


        private static void MapAccesDenied(this RouteGroupBuilder builder)
        {
            builder.MapGet("/accessdenied", () =>
            {
                return Results.Json(statusCode: 403, data: "Error 403: Access Denied");
            })
                .Produces<string>(statusCode: 403)
                .WithName("403 Denied");
        }

        private static void MapLogin(this RouteGroupBuilder builder)
        {
            builder.MapPost("/login", (HttpRequest request, AuthOptionsService authOptions, IUserService userService) =>
            {
                // получаем из формы email и пароль
                var form = request.Form;
                // если email и/или пароль не установлены, посылаем статусный код ошибки 400
                if (!form.ContainsKey("email") || !form.ContainsKey("password"))
                    return Results.BadRequest("Email и/или пароль не установлены");

                LoginDTO data = FormMapper.Map<LoginDTO>(form);

                // находим пользователя
                User? person = userService.FindPersonWithPassword(data);
                // если пользователь не найден, отправляем статусный код 401
                if (person is null) return Results.Unauthorized();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, person.Email),
                    new Claim(ClaimTypes.Role, person.Role),
                };

                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: authOptions.ISSUER,
                        audience: authOptions.AUDIENCE,
                        claims: claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                        signingCredentials: new SigningCredentials(authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                return Results.Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
            })
                .Accepts<LoginDTO>(contentType: HttpContentTypes.MultipatFormdata)
                .Produces<string>(statusCode: StatusCodes.Status200OK)
                .WithName("auth login")
                .WithDescription("login endpoint");
        }

        private static void MapRegister(this RouteGroupBuilder builder)
        {
            builder.MapPost("/register", (HttpRequest request, AuthOptionsService authOptions, IUserService userService) =>
            {
                // получаем из формы email и пароль
                var form = request.Form;
                // если email и/или пароль не установлены, посылаем статусный код ошибки 400
                if (!form.ContainsKey("email") || !form.ContainsKey("password"))
                    return Results.BadRequest("Email и/или пароль не установлены");

                LoginDTO data = FormMapper.Map<LoginDTO>(form);

                bool created = userService.CreateUser(data);

                if (created) return Results.Json<string>(statusCode: StatusCodes.Status400BadRequest, data: $"User {data.Email} not created, already exists");

                return Results.Json<string>(statusCode: StatusCodes.Status201Created, data: $"User {data.Email} created!");
            })
                .Accepts<LoginDTO>(contentType: HttpContentTypes.MultipatFormdata)
                .Produces<string>(statusCode: StatusCodes.Status201Created)
                .WithName("auth register")
                .WithDescription("register endpoint");
        }

        public static RouteGroupBuilder MapAuthEndpoints(this WebApplication app, string authRouteBase = "/api/auth")
        {
            var builder = app.MapGroup(authRouteBase);

            builder.MapAccesDenied();
            builder.MapLogin();
            builder.MapRegister();

            builder
                .WithTags("auth")
                .WithOpenApi();

            return builder;
        }
    }
}