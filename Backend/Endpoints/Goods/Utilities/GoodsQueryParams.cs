using Backend.DTOs.GoodsFiltering;

namespace Backend.Endpoints.Goods.Utilities
{
    public static class GoodsQueryParams
    {
        public static Delegate Handler<T_Service, T_Result>(Func<GoodsQueryParamsDTO, T_Service, T_Result> handler) where T_Service : class =>
            (T_Service service, string? NameSearch, int? CategoryId, int[]? CategoriesList, decimal? MinPrice, decimal? MaxPrice, string? OrderBy, bool? OrderByDescending, int? PageIndex, int? PageSize, bool? WithAmount) =>
                handler(
                   new(
                       NameSearch: NameSearch,
                       Categories: (CategoryId.HasValue || CategoriesList?.Length > 0) ? new(CategoryId, CategoriesList) : null,
                       Price: (MinPrice.HasValue || MaxPrice.HasValue) ? new(MinPrice, MaxPrice) : null,
                       Ordering: (OrderBy is not null || OrderByDescending.HasValue) ? new(OrderBy, OrderByDescending ?? false) : null,
                       Pagination: (PageIndex.HasValue || PageSize.HasValue) ? new(PageIndex, PageSize) : null,
                       WithAmount: WithAmount ?? false
                   ),
                   service
               );

        public static Delegate Handler<T_Service1, T_Service2, T_Result>(Func<GoodsQueryParamsDTO, T_Service1, T_Service2, T_Result> handler) where T_Service1 : class where T_Service2 : class =>
            (T_Service1 service1, T_Service2 service2, string? NameSearch, int? CategoryId, int[]? CategoriesList, decimal? MinPrice, decimal? MaxPrice, string? OrderBy, bool? OrderByDescending, int? PageIndex, int? PageSize, bool? WithAmount) =>
                handler(
                   new(
                       NameSearch: NameSearch,
                       Categories: (CategoryId.HasValue || CategoriesList?.Length > 0) ? new(CategoryId, CategoriesList) : null,
                       Price: (MinPrice.HasValue || MaxPrice.HasValue) ? new(MinPrice, MaxPrice) : null,
                       Ordering: (OrderBy is not null || OrderByDescending.HasValue) ? new(OrderBy, OrderByDescending ?? false) : null,
                       Pagination: (PageIndex.HasValue || PageSize.HasValue) ? new(PageIndex, PageSize) : null,
                       WithAmount: WithAmount ?? false
                   ),
                   service1, service2
               );

        public static Delegate Handler<T_Service1, T_Service2, T_Service3, T_Result>(Func<GoodsQueryParamsDTO, T_Service1, T_Service2, T_Service3, T_Result> handler) where T_Service1 : class where T_Service2 : class where T_Service3 : class =>
            (T_Service1 service1, T_Service2 service2, T_Service3 service3, string? NameSearch, int? CategoryId, int[]? CategoriesList, decimal? MinPrice, decimal? MaxPrice, string? OrderBy, bool? OrderByDescending, int? PageIndex, int? PageSize, bool? WithAmount) =>
                handler(
                   new(
                       NameSearch: NameSearch,
                       Categories: (CategoryId.HasValue || CategoriesList?.Length > 0) ? new(CategoryId, CategoriesList) : null,
                       Price: (MinPrice.HasValue || MaxPrice.HasValue) ? new(MinPrice, MaxPrice) : null,
                       Ordering: (OrderBy is not null || OrderByDescending.HasValue) ? new(OrderBy, OrderByDescending ?? false) : null,
                       Pagination: (PageIndex.HasValue || PageSize.HasValue) ? new(PageIndex, PageSize) : null,
                       WithAmount: WithAmount ?? false
                   ),
                   service1, service2, service3
               );
    }
}