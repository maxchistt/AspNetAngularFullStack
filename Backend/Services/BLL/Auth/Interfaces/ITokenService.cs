namespace Backend.Services.BLL.Auth.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string claimName, string claimRole);
    }
}