using Backend.Services.Auth.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Backend.Services.Auth
{
    public class TokenOptions : ITokenOptions
    {
        public string ISSUER { get; private set; } = "MyAuthServer"; // издатель токена
        public string AUDIENCE { get; private set; } = "MyAuthClient"; // потребитель токена
        private string KEY { get; set; } = "mysupersecret_secretkey!123";   // ключ для шифрации

        public TokenOptions(IConfiguration configuration)
        {
            ISSUER = configuration["Jwt:Issuer"] ?? ISSUER;
            AUDIENCE = configuration["Jwt:Audience"] ?? AUDIENCE;
            KEY = configuration["Jwt:Key"] ?? KEY;
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}