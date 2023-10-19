using Backend.Models.Goods;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Goods
{
    public record ProductDataDTO
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = null;

        [Required]
        public decimal Price { get; set; } = decimal.Zero;

        public int? Amount { get; set; } = null;

        public ProductDataDTO() { }
        public ProductDataDTO(Product product)
        {
            CategoryId = product.CategoryId;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Amount = product.Inventory?.Amount;
        }
    }
}