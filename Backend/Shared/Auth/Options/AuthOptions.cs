using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Backend.Shared.Auth.Options
{
    public static class AuthOptions
    {
        public static string ISSUER { get; private set; } = "MyAuthServer"; // издатель токена
        public static string AUDIENCE { get; private set; } = "MyAuthClient"; // потребитель токена
        private static string KEY { get; set; } = "mysupersecret_secretkey!123";   // ключ для шифрации

        public static void ApplyConfiguration(IConfiguration configuration)
        {
            ISSUER = configuration["Jwt:Issuer"] ?? ISSUER;
            AUDIENCE = configuration["Jwt:Audience"] ?? AUDIENCE;
            KEY = configuration["Jwt:Key"] ?? KEY;
        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}