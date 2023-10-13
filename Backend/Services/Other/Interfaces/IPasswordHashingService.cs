namespace Backend.Services.Other.Interfaces
{
    public interface IPasswordHashingService
    {
        string HashPassword(string password);

        bool Verify(string password, string hash);
    }
}