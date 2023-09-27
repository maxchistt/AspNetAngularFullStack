using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Backend.EF.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Email { get; set; }

        public string Password { get; set; }
        public string Role { get; set; }

        public User()
        { }

        public User(string email, string password, string role)
        {
            Email = email;
            Password = password;
            Role = role;
        }
    }
}