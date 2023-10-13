using Microsoft.IdentityModel.Tokens;

namespace Backend.Services.Other.Interfaces
{
    public interface ITokenOptions
    {
        string AUDIENCE { get; }
        string ISSUER { get; }

        SymmetricSecurityKey GetSymmetricSecurityKey();
    }
}