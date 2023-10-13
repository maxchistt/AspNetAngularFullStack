using Backend.DTOs.Auth;

namespace Backend.Services.BLL.Auth.Interfaces
{
    public interface IAuthService
    {
        string? Login(LoginDTO data);

        bool PasswordChange(PasswordResetDTO data);

        bool Register(LoginDTO data);
    }
}