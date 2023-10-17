using Backend.Models.Goods;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Goods
{
    public record ProductDataWithCategoryDTO : ProductDataWithoutCategoryDTO
    {
        [Required]
        public CategoryDTO Category { get; set; } = null!;

        public ProductDataWithCategoryDTO() : base() { }
        public ProductDataWithCategoryDTO(Product product) : base(product)
        {
            if (product.Category is not null) Category = (CategoryDTO)product.Category;
        }

        public static implicit operator ProductDataDTO(ProductDataWithCategoryDTO product) => new ProductDataDTO(product);
    }
}