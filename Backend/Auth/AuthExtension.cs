using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Backend.Auth
{
    public static class AuthExtension
    {
        public static void AddAuth(this IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
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
            app.UseAuthentication();
            app.UseAuthorization();
        }

        public static RouteGroupBuilder MapAuthEndpoints(this WebApplication app, string authRouteBase = "/api/auth")
        {
            return app.MapGroup(authRouteBase).MapAuthEndpointsGroup();
        }

        private static RouteGroupBuilder MapAuthEndpointsGroup(this RouteGroupBuilder builder)
        {
            builder.MapPost("/login", () =>
            {
                string username = "TestUser";
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        claims: claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                return new JwtSecurityTokenHandler().WriteToken(jwt);
            })
                .WithName("auth login")
                .WithDescription("login endpoint");

            builder
                .WithTags("auth")
                .WithOpenApi();

            return builder;
        }
    }
}