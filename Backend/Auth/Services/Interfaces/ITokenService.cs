namespace Backend.Auth.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string claimName, string claimRole);
    }
}