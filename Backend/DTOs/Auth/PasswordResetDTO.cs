namespace Backend.DTOs.Auth
{
    public record PasswordResetDTO : LoginDTO
    {
        public string NewPassword { get; set; } = string.Empty;
    }
}