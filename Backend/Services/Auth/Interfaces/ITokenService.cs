namespace Backend.Services.Auth.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string claimName, string claimRole);
    }
}