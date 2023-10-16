using Backend.DTOs.Goods;
using Backend.DTOs.Other;
using Backend.Models.Goods;

namespace Backend.Services.DAL.Interfaces
{
    public interface IGoodsService
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<List<Product>> GetGoodsAsync(GoodsFilteringParamsDto? filter = null);
        Task<ProductInventory?> GetInventoryAsync(int productId);
        Task<ProductInventory?> GetInventoryAsync(Product product);
        Task<PaginatedListDTO<Product>> GetPaginatedGoodsAsync(GoodsFilteringParamsDto goodsFilteringParams);
        Task<int> GetTotalCount();
        Task LoadInventoryAsync(Product product);

        Task<bool> AddCategoryAsync(Category category);

        Task<bool> AddProductAsync(Product product);
    }
}