using Backend.Models.Goods;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Goods
{
    public record ProductDataWithoutCategoryDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = null;

        [Required]
        public decimal Price { get; set; } = decimal.Zero;

        public ProductDataWithoutCategoryDTO() { }

        public ProductDataWithoutCategoryDTO(Product product)
        {
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
        }
    }
}
