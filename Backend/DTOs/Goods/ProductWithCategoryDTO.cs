using Backend.Models.Goods;

namespace Backend.DTOs.Goods
{
    public record ProductWithCategoryDTO : ProductDataWithCategoryDTO
    {
        public int Id { get; init; }

        public ProductWithCategoryDTO() : base() { }
        public ProductWithCategoryDTO(Product product) : base(product) { 
            Id = product.Id;
        }

        public static explicit operator ProductWithCategoryDTO(Product product) => new ProductWithCategoryDTO(product);

        public static implicit operator ProductDTO(ProductWithCategoryDTO product)=> new ProductDTO() {Id = product.Id, CategoryId = product.Category.Id, Name = product.Name, Description = product.Description, Price = product.Price };
        
    }
}
