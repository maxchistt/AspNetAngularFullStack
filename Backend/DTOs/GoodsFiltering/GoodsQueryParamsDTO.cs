namespace Backend.DTOs.GoodsFiltering
{
    public record GoodsQueryParamsDTO(
        string? NameSearch = null,
        CategoriesFilteringDTO? Categories = null,
        PriceFilteringDTO? Price = null,
        OrderingDTO? Ordering = null,
        PaginationDTO? Pagination = null,
        bool WithAmount = false
    );
}