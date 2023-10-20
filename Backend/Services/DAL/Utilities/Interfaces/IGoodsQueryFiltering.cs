using Backend.DTOs.GoodsFiltering;
using Backend.Models.Goods;

namespace Backend.Services.DAL.Utilities.Interfaces
{
    public interface IGoodsQueryFiltering
    {
        IQueryable<Product> FilterByCategory(IQueryable<Product> query, CategoriesFilteringDTO categories);
        IQueryable<Product> FilterByName(IQueryable<Product> query, string nameSearch);
        IQueryable<Product> FilterByPrice(IQueryable<Product> query, PriceFilteringDTO price);
    }
}