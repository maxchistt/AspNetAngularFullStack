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
    public static class AuthExtension
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            AuthOptionsService authOptions = new AuthOptionsService(configuration);
            services.AddSingleton<AuthOptionsService>(authOptions);
            services.AddSingleton<IUserService, UserService>();
            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = authOptions.ISSUER,
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = authOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
                        // установка ключа безопасности
                        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });
        }

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

        public static RouteGroupBuilder MapAuthEndpoints(this WebApplication app, string authRouteBase = "/api/auth")
        {
            var builder = app.MapGroup(authRouteBase);

            builder.MapGet("/accessdenied", () =>
            {
                return Results.Json(statusCode: 403, data: "Error 403: Access Denied");
            })
                .Produces<string>(statusCode: 403)
                .WithName("403 Denied");

            builder.MapPost("/login", (HttpRequest request, AuthOptionsService authOptions, IUserService userService) =>
            {
                // получаем из формы email и пароль
                var form = request.Form;
                // если email и/или пароль не установлены, посылаем статусный код ошибки 400
                if (!form.ContainsKey("email") || !form.ContainsKey("password"))
                    return Results.BadRequest("Email и/или пароль не установлены");

                LoginDTO data = FormMapper.Map<LoginDTO>(form);

                // находим пользователя
                User? person = userService.FindPerson(data);
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

            builder
                .WithTags("auth")
                .WithOpenApi();

            return builder;
        }
    }
}