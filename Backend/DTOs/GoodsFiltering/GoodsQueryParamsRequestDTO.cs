namespace Backend.DTOs.GoodsFiltering
{
    public record GoodsQueryParamsRequestDTO(
        string? NameSearch = null,

        int? CategoryId = null,
        IEnumerable<int>? CategoriesIdList = null,

        decimal? MinPrice = null,
        decimal? MaxPrice = null,

        string? OrderBy = null,
        bool? OrderByDescending = null,

        int? PageIndex = null,
        int? PageSize = null,

        bool? WithAmount = null
    );
}