using Microsoft.IdentityModel.Tokens;

namespace Backend.Auth.Services.Interfaces
{
    public interface IAuthOptionsService
    {
        string AUDIENCE { get; }
        string ISSUER { get; }

        SymmetricSecurityKey GetSymmetricSecurityKey();
    }
}