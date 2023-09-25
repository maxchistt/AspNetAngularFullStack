using Backend.Auth.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Backend.Auth.Services
{
    public class TokenService : ITokenService
    {
        private IAuthOptionsService _authOptions;

        public TokenService(IAuthOptionsService authOptions)
        {
            _authOptions = authOptions;
        }

        public string GenerateToken(string claimName, string claimRole)
        {
            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, claimName),
                    new Claim(ClaimTypes.Role, claimRole),
            };

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: _authOptions.ISSUER,
                    audience: _authOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}