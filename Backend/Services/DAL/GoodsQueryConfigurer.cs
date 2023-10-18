using Backend.DTOs.GoodsFiltering;
using Backend.EF.Extensions;
using Backend.Models.Goods;
using Backend.Services.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Backend.Services.DAL
{
    public class GoodsQueryConfigurer : IGoodsQueryConfigurer
    {
        public IQueryable<Product> GetFilteredQuery(IQueryable<Product> query, GoodsQueryParamsDTO queryParams)
        {
            if (queryParams?.Categories?.CategoriesIdList is null && queryParams?.Categories?.CategoryId is null) return query;

            CategoriesFilteringDTO categories = queryParams.Categories;

            IEnumerable<int> idList = categories.CategoriesIdList ?? Enumerable.Empty<int>();

            if (categories.CategoryId.HasValue && !idList.Contains(categories.CategoryId.Value))
                idList = idList.Append(categories.CategoryId.Value);

            if (idList.Count() > 1)
            {
                query = query.Where(p => idList.Contains(p.CategoryId));
            }
            else if (idList.Count() > 0)
            {
                query = query.Where(p => p.CategoryId == idList.First());
            }
            return query;
        }

        public IQueryable<Product> GetOrderedQuery(IQueryable<Product> query, GoodsQueryParamsDTO queryParams)
        {
            if (!string.IsNullOrEmpty(queryParams?.Ordering?.OrderBy))
            {
                var parameter = Expression.Parameter(typeof(Product), "x");
                var property = Expression.Property(parameter, queryParams.Ordering.OrderBy);
                var lambda = Expression.Lambda(property, parameter);

                query = query.OrderBy((Expression<Func<Product, object>>)lambda);
            }
            if (queryParams?.Ordering?.OrderByDescending is true)
            {
                query = query.OrderDescending();
            }

            return query;
        }

        public IQueryable<Product> GetIncludedQuery(IQueryable<Product> query, GoodsQueryParamsDTO queryParams)
        {
            if (queryParams?.WithAmount ?? false)
                query = query.Include(p => p.Inventory);
            return query;
        }

        public IQueryable<Product> GetPaginatedQuery(IQueryable<Product> query, GoodsQueryParamsDTO queryParams)
        {
            if (queryParams?.Pagination?.PageIndex is not null && queryParams?.Pagination?.PageSize is not null)
                query = query.GetPaginated(queryParams.Pagination.PageIndex.Value, queryParams.Pagination.PageSize.Value);
            return query;
        }
    }
}
