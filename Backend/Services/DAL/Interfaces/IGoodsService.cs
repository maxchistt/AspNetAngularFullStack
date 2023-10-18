using Backend.DTOs.GoodsFiltering;
using Backend.Models.Goods;
using Backend.Shared.Other;

namespace Backend.Services.DAL.Interfaces
{
    public interface IGoodsService
    {
        Task<List<Category>> GetCategoriesAsync();

        Task<List<Product>> GetGoodsAsync(GoodsQueryParamsDTO? queryParams = null);

        Task<Product?> GetProductAsync(int productId);

        Task<ProductInventory?> GetInventoryAsync(int productId);

        Task<ProductInventory?> GetInventoryAsync(Product product);

        Task<PaginatedList<Product>> GetPaginatedGoodsAsync(GoodsQueryParamsDTO queryParams);

        Task<int> GetTotalCount(GoodsQueryParamsDTO queryParams);

        Task LoadInventoryAsync(Product product);

        Task<bool> AddCategoryAsync(Category category);

        Task<bool> AddProductAsync(Product product);
    }
}