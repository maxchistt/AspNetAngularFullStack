using Backend.DTOs.GoodsFiltering;
using Backend.Models.Goods;
using Backend.Services.DAL.Auxiliary.Interfaces;

namespace Backend.Services.DAL.Auxiliary
{
    public class GoodsQueryFiltering : IGoodsQueryFiltering
    {
        public IQueryable<Product> FilterByCategory(IQueryable<Product> query, CategoriesFilteringDTO categories)
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

        public IQueryable<Product> FilterByPrice(IQueryable<Product> query, PriceFilteringDTO price)
        {
            if (price.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= price.MaxPrice.Value);
            if (price.MinPrice.HasValue)
                query = query.Where(p => p.Price >= price.MinPrice.Value);
            return query;
        }

        public IQueryable<Product> FilterByName(IQueryable<Product> query, string nameSearch)
        {
            return query.Where(p => p.Name.Contains(nameSearch));
        }
    }
}