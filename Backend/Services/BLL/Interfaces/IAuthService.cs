using Backend.DTOs.Auth;

namespace Backend.Services.BLL.Interfaces
{
    public interface IAuthService
    {
        string? Login(LoginDTO data);

        bool PasswordChange(PasswordResetDTO data);

        bool Register(LoginDTO data);
    }
}