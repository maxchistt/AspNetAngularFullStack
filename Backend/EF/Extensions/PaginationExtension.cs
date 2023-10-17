using System.Linq.Expressions;

namespace Backend.EF.Extensions
{
    public static class PaginationExtension
    {
        public static IQueryable<TSource> GetPaginated<TSource, TKey>(this IQueryable<TSource> query, Expression<Func<TSource, TKey>> orderByExpression, int pageIndex, int pageSize) where TSource : class
            => query.GetPaginated(pageIndex, pageSize, orderByExpression);

        public static IQueryable<TSource> GetPaginated<TSource, TKey>(this IQueryable<TSource> query, int pageIndex, int pageSize, Expression<Func<TSource, TKey>> orderByExpression) where TSource : class
            => query
                .OrderBy(orderByExpression) // You can order by any property you prefer
                .GetPaginated<TSource>(pageIndex, pageSize);

        public static IQueryable<TSource> GetPaginated<TSource>(this IQueryable<TSource> query, int pageIndex, int pageSize) where TSource : class
            => query
                .Skip((pageIndex - 1) * pageSize) // Calculate how many items to skip based on pageIndex
                .Take(pageSize);

        public static IEnumerable<TSource> GetPaginatedEnumerableByFunc<TSource, TKey>(this IQueryable<TSource> query, Func<TSource, TKey> orderByExpression, int pageIndex, int pageSize) where TSource : class
            => query.OrderBy(orderByExpression) // You can order by any property you prefer
               .Skip((pageIndex - 1) * pageSize) // Calculate how many items to skip based on pageIndex
               .Take(pageSize);
    }
}