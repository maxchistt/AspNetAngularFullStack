using Backend.Models.Goods;

namespace Backend.DTOs.Goods
{
    public record ProductDTO:ProductDataDTO
    {
        public int Id { get; set;}

        public ProductDTO() : base() { }
        public ProductDTO(Product product) : base(product)
        {
            Id = product.Id;
        }

        public static explicit operator ProductDTO(Product product) =>new ProductDTO(product);
    }
}
