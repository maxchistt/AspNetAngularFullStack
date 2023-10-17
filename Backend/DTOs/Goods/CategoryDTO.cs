using Backend.Models.Goods;

namespace Backend.DTOs.Goods
{
    public record CategoryDTO : CategoryDataDTO
    {
        public int Id { get; init; }

        public CategoryDTO() : base() { }

        public CategoryDTO(Category category) : base(category)
        {
            Id = category.Id;
        }

        public static explicit operator CategoryDTO(Category category) => new CategoryDTO(category);
    }
}