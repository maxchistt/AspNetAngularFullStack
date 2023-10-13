namespace Backend.Services.DAL.Users.Interfaces
{
    public interface IPasswordHashingService
    {
        string HashPassword(string password);

        bool Verify(string password, string hash);
    }
}