namespace Backend.Services.BLL.Utilities.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string claimName, string claimRole);
    }
}