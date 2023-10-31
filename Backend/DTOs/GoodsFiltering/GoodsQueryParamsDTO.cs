namespace Backend.DTOs.GoodsFiltering
{
    public record GoodsQueryParamsDTO(
        string? NameSearch = null,
        CategoriesFilteringDTO? Categories = null,
        PriceFilteringDTO? Price = null,
        OrderingDTO? Ordering = null,
        PaginationDTO? Pagination = null,
        bool WithAmount = false
    )
    {
        public static explicit operator GoodsQueryParamsDTO(GoodsQueryParamsRequestDTO request) =>
            new GoodsQueryParamsDTO(
                NameSearch: request.NameSearch,
                Categories: (request.CategoryId.HasValue || request.CategoriesIdList?.Count() > 0) ? new(request.CategoryId, request.CategoriesIdList) : null,
                Price: (request.MinPrice.HasValue || request.MaxPrice.HasValue) ? new(request.MinPrice, request.MaxPrice) : null,
                Ordering: (request.OrderBy is not null || request.OrderByDescending.HasValue) ? new(request.OrderBy, request.OrderByDescending ?? false) : null,
                Pagination: (request.PageIndex.HasValue || request.PageSize.HasValue) ? new(request.PageIndex, request.PageSize) : null,
                WithAmount: request.WithAmount ?? false
            );
    };
}