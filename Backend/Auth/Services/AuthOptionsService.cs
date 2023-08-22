using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Backend.Auth.Services
{
    public class AuthOptionsService
    {
        public string ISSUER { get; } = "MyAuthServer"; // издатель токена
        public string AUDIENCE { get; } = "MyAuthClient"; // потребитель токена
        private string KEY { get; } = "mysupersecret_secretkey!123";   // ключ для шифрации

        public AuthOptionsService(IConfiguration configuration)
        {
            ISSUER = configuration["Jwt:Issuer"] ?? ISSUER;
            AUDIENCE = configuration["Jwt:Audience"] ?? AUDIENCE;
            KEY = configuration["Jwt:Key"] ?? KEY;
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}