using Backend.Services.Other.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Backend.Services.Other
{
    public class TokenService : ITokenService
    {
        private ITokenOptions _tokenOptions;

        public TokenService(ITokenOptions tokenOptions)
        {
            _tokenOptions = tokenOptions;
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
                    issuer: _tokenOptions.ISSUER,
                    audience: _tokenOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(_tokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}