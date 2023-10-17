using Backend.Models.Goods;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Goods
{
    public record CategoryDataDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public CategoryDataDTO() { }

        public CategoryDataDTO(Category category)
        {
            Name = category.Name;
        }

        public static explicit operator CategoryDataDTO(Category category)=> new CategoryDataDTO(category);
    }
}