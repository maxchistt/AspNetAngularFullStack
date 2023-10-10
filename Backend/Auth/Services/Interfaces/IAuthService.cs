using Backend.Auth.DTOs;

namespace Backend.Auth.Services.Interfaces
{
    public interface IAuthService
    {
        string? Login(LoginDTO data);
        bool PasswordChange(PasswordResetDTO data);
        bool Register(LoginDTO data);
    }
}