using Backend.Models.Goods;

namespace Backend.DTOs.Goods
{
    public record ProductWithAmountDTO:ProductDTO
    {
        int Amount { get; set; } = 0;
        public ProductWithAmountDTO() : base() { }
        public ProductWithAmountDTO(Product product) : base(product)
        {
            Amount = product.Inventory?.Amount ?? Amount;
        }

        public static explicit operator ProductWithAmountDTO(Product product) => new ProductWithAmountDTO(product);
    }
}
