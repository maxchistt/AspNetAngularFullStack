using Backend.Models.Goods;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Goods
{
    public record ProductDataDTO : ProductDataWithoutCategoryDTO
    {
        [Required]
        public int CategoryId { get; set; }

        public ProductDataDTO() : base() { }
        public ProductDataDTO(Product product) : base(product)
        {
            CategoryId = product.CategoryId;
        }

        public ProductDataDTO(ProductDataWithCategoryDTO product) : base(product as ProductDataWithoutCategoryDTO)
        {
            CategoryId = product.Category.Id;
        }
    }
}