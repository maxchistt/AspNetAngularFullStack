using Backend.DTOs.GoodsFiltering;
using Backend.Models.Goods;
using Backend.Shared.Other;

namespace Backend.Services.DAL.Interfaces
{
    public interface IGoodsService
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<List<Product>> GetGoodsAsync(GoodsFilteringParamsDTO? filter = null);
        Task<ProductInventory?> GetInventoryAsync(int productId);
        Task<ProductInventory?> GetInventoryAsync(Product product);
        Task<PaginatedList<Product>> GetPaginatedGoodsAsync(GoodsFilteringParamsDTO goodsFilteringParams);
        Task<int> GetTotalCount();
        Task LoadInventoryAsync(Product product);

        Task<bool> AddCategoryAsync(Category category);

        Task<bool> AddProductAsync(Product product);
    }
}