using Microsoft.IdentityModel.Tokens;

namespace Backend.Services.BLL.Utilities.Interfaces
{
    public interface ITokenOptions
    {
        string AUDIENCE { get; }
        string ISSUER { get; }

        SymmetricSecurityKey GetSymmetricSecurityKey();
    }
}