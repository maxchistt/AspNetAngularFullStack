using Backend.DTOs.GoodsFiltering;
using Backend.EF.Context;
using Backend.Models.Goods;
using Backend.Services.DAL.Interfaces;
using Backend.Shared.Other;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.DAL
{
    public class GoodsService : IGoodsService
    {
        private DataContext Context { get; }
        private IGoodsQueryConfigurer QueryConfigurer { get; }

        public GoodsService(DataContext context, IGoodsQueryConfigurer queryConfigurer)
        {
            Context = context;
            QueryConfigurer = queryConfigurer;
        }

        public Task<int> GetTotalCount(GoodsQueryParamsDTO queryParams)
        {
            var query = Context.Goods.AsQueryable();

            if (queryParams is not null)
            {
                query = QueryConfigurer.GetFilteredQuery(query, queryParams);
            }

            return query.CountAsync();
        }

        public Task<List<Product>> GetGoodsAsync(GoodsQueryParamsDTO? queryParams = default)
        {
            var query = Context.Goods.AsQueryable();

            if (queryParams is not null)
            {
                query = QueryConfigurer.GetFilteredQuery(query, queryParams);
                query = QueryConfigurer.GetOrderedQuery(query, queryParams);
                query = QueryConfigurer.GetIncludedQuery(query, queryParams);
                query = QueryConfigurer.GetPaginatedQuery(query, queryParams);
            }

            return query.ToListAsync();
        }

        public async Task<PaginatedList<Product>> GetPaginatedGoodsAsync(GoodsQueryParamsDTO queryParams)
        {
            var totalItemCountTask = GetTotalCount(queryParams);

            var goodsTask = GetGoodsAsync(queryParams);

            await Task.WhenAll(totalItemCountTask, goodsTask);

            int index = queryParams.Pagination?.PageIndex.HasValue ?? false ? queryParams.Pagination.PageIndex.Value : 1;
            int size = queryParams.Pagination?.PageSize.HasValue ?? false ? queryParams.Pagination.PageSize.Value : totalItemCountTask.Result;

            return new PaginatedList<Product>(goodsTask.Result, totalItemCountTask.Result, index, size);
        }

        public Task<List<Category>> GetCategoriesAsync()
        {
            return Context.Categories.ToListAsync();
        }

        public async Task<ProductInventory?> GetInventoryAsync(Product product)
        {
            await LoadInventoryAsync(product);

            return product.Inventory;
        }

        public Task<ProductInventory?> GetInventoryAsync(int productId)
        {
            return Context.GoodsInventories.Where(i => i.ProductId == productId).FirstOrDefaultAsync();
        }

        public Task LoadInventoryAsync(Product product)
        {
            return product.Inventory is null
                ? Context.Entry(product).Reference(p => p.Inventory).LoadAsync()
                : Task.CompletedTask;
        }

        public async Task<bool> AddCategoryAsync(Category category)
        {
            try
            {
                Context.Categories.Add(category);
                await Context.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            try
            {
                Context.Goods.Add(product);
                await Context.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<Product?> GetProductAsync(int productId)
        {
            return await Context.Goods.FindAsync(productId);
        }
    }
}