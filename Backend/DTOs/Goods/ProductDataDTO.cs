using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Goods
{
    public record ProductDataDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = null;

        [Required]
        public decimal Price { get; set; } = decimal.Zero;

        [Required]
        public int CategoryId { get; set; } = 0;
    }
}
