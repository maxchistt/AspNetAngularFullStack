using Microsoft.IdentityModel.Tokens;

namespace Backend.Services.Auth.Interfaces
{
    public interface ITokenOptions
    {
        string AUDIENCE { get; }
        string ISSUER { get; }

        SymmetricSecurityKey GetSymmetricSecurityKey();
    }
}