namespace Backend.Auth.DTOs
{
    public record PasswordResetDTO : LoginDTO
    {
        public string NewPassword { get; set; } = string.Empty;
    }
}