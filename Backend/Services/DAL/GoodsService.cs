﻿using Backend.DTOs.Goods;
using Backend.EF.Context;
using Backend.EF.Extensions;
using Backend.Models.Goods;
using Backend.Services.DAL.Interfaces;
using Backend.Shared.Other;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Backend.Services.DAL
{
    public class GoodsService : IGoodsService
    {
        private DataContext Context { get; }

        public GoodsService(DataContext context)
        { Context = context; }

        public Task<int> GetTotalCount() => Context.Goods.CountAsync();

        public Task<List<Product>> GetGoodsAsync(GoodsFilteringParamsDTO? filter = default)
        {
            var query = Context.Goods.AsQueryable();

            if (filter?.CategoryId is not null)
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);

            if (filter?.orderBy is not null and not "")
            {
                var parameter = Expression.Parameter(typeof(Product), "x");
                var property = Expression.Property(parameter, filter.orderBy);
                var lambda = Expression.Lambda(property, parameter);

                query = query.OrderBy((Expression<Func<Product, object>>)lambda);
            }

            if (filter?.PageIndex is not null && filter?.PageSize is not null)
                query = query.GetPaginated(filter.PageIndex.Value, filter.PageSize.Value);

            if (filter?.WithAmount ?? false)
                query = query.Include(p => p.Inventory);

            return query.ToListAsync();
        }

        public async Task<PaginatedList<Product>> GetPaginatedGoodsAsync(GoodsFilteringParamsDTO goodsFilteringParams)
        {
            var totalItemCountTask = GetTotalCount();

            var goodsTask = GetGoodsAsync(goodsFilteringParams);

            await Task.WhenAll(totalItemCountTask, goodsTask);

            int index = goodsFilteringParams.PageIndex.HasValue ? goodsFilteringParams.PageIndex.Value : 1;
            int size = goodsFilteringParams.PageSize.HasValue ? goodsFilteringParams.PageSize.Value : totalItemCountTask.Result;

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
    }
}