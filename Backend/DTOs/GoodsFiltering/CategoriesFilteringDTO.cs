namespace Backend.DTOs.GoodsFiltering
{
    public record CategoriesFilteringDTO(
        int? CategoryId = null,
        IEnumerable<int>? CategoriesIdList = null
    );
}