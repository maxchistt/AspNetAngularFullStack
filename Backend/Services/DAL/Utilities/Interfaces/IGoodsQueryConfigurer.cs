using Backend.DTOs.GoodsFiltering;
using Backend.Models.Goods;

namespace Backend.Services.DAL.Utilities.Interfaces
{
    public interface IGoodsQueryConfigurer
    {
        IQueryable<Product> GetFilteredQuery(IQueryable<Product> query, GoodsQueryParamsDTO queryParams);
        IQueryable<Product> GetIncludedQuery(IQueryable<Product> query, GoodsQueryParamsDTO queryParams);
        IQueryable<Product> GetOrderedQuery(IQueryable<Product> query, GoodsQueryParamsDTO queryParams);
        IQueryable<Product> GetPaginatedQuery(IQueryable<Product> query, GoodsQueryParamsDTO queryParams);
    }
}