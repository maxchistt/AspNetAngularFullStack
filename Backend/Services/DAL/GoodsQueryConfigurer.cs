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
        private IQueryable<Product> FilterByCategory(IQueryable<Product> query, CategoriesFilteringDTO categories)
        {
            if (categories.CategoriesIdList is null && categories.CategoryId is null) return query;

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

        private IQueryable<Product> FilterByPrice(IQueryable<Product> query, PriceFilteringDTO price)
        {
            if (price.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= price.MaxPrice.Value);
            if (price.MinPrice.HasValue)
                query = query.Where(p => p.Price >= price.MinPrice.Value);
            return query;
        }

        private IQueryable<Product> FilterByName(IQueryable<Product> query, string nameSearch)
        {
            return query.Where(p => p.Name.Contains(nameSearch));
        }

        public IQueryable<Product> GetFilteredQuery(IQueryable<Product> query, GoodsQueryParamsDTO queryParams)
        {
            if (queryParams.Categories is not null)
                query = FilterByCategory(query, queryParams.Categories);
            if (!string.IsNullOrEmpty(queryParams.NameSearch))
                query = FilterByName(query, queryParams.NameSearch);
            if (queryParams.Price is not null)
                query = FilterByPrice(query, queryParams.Price);
            return query;
        }

        public IQueryable<Product> GetOrderedQuery(IQueryable<Product> query, GoodsQueryParamsDTO queryParams)
        {
            if (!string.IsNullOrEmpty(queryParams?.Ordering?.OrderBy))
            {
                string OrderingProperty = queryParams.Ordering.OrderBy;

                var parameter = Expression.Parameter(typeof(Product), "x");
                var property = Expression.Property(parameter, OrderingProperty);
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