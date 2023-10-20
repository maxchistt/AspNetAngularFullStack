using Microsoft.IdentityModel.Tokens;

namespace Backend.Services.Utilities.BLL.Interfaces
{
    public interface ITokenOptions
    {
        string AUDIENCE { get; }
        string ISSUER { get; }

        SymmetricSecurityKey GetSymmetricSecurityKey();
    }
}