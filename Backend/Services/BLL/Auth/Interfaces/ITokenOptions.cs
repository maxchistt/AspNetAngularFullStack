using Microsoft.IdentityModel.Tokens;

namespace Backend.Services.BLL.Auth.Interfaces
{
    public interface ITokenOptions
    {
        string AUDIENCE { get; }
        string ISSUER { get; }

        SymmetricSecurityKey GetSymmetricSecurityKey();
    }
}