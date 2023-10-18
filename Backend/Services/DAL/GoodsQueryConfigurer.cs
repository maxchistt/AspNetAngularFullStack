using Backend.DTOs.GoodsFiltering;
using Backend.EF.Extensions;
using Backend.Models.Goods;
using Backend.Services.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.DAL
{
    public class GoodsQueryConfigurer : IGoodsQueryConfigurer
    {
        private IProductOrderingExpressionParcer ExpressionParcer { get; }
        private IGoodsQueryFiltering QueryFiltering { get; }

        public GoodsQueryConfigurer(IProductOrderingExpressionParcer parcer, IGoodsQueryFiltering filtering)
        {
            ExpressionParcer = parcer;
            QueryFiltering = filtering;
        }

        public IQueryable<Product> GetFilteredQuery(IQueryable<Product> query, GoodsQueryParamsDTO queryParams)
        {
            if (queryParams.Categories is not null)
                query = QueryFiltering.FilterByCategory(query, queryParams.Categories);
            if (!string.IsNullOrEmpty(queryParams.NameSearch))
                query = QueryFiltering.FilterByName(query, queryParams.NameSearch);
            if (queryParams.Price is not null)
                query = QueryFiltering.FilterByPrice(query, queryParams.Price);
            return query;
        }

        public IQueryable<Product> GetOrderedQuery(IQueryable<Product> query, GoodsQueryParamsDTO queryParams)
        {
            if (!string.IsNullOrEmpty(queryParams?.Ordering?.OrderBy))
            {
                string OrderingProperty = queryParams.Ordering.OrderBy;

                var expression = ExpressionParcer.GetExpression(OrderingProperty);

                if (expression is not null)
                    query = query.OrderBy(expression);

                if (queryParams?.Ordering?.OrderByDescending is true)
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