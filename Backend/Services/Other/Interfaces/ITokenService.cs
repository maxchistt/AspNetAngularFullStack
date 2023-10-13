namespace Backend.Services.Other.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string claimName, string claimRole);
    }
}