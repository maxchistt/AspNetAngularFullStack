using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Goods
{
    public record CategoryDataDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
