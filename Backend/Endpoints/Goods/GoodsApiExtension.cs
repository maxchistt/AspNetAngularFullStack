﻿using Backend.DTOs.Goods;
using Backend.DTOs.GoodsFiltering;
using Backend.Models.Goods;
using Backend.Services.DAL.Interfaces;
using Backend.Shared.Other;

namespace Backend.Endpoints.Goods
{
    public static class GoodsApiExtension
    {
        public static RouteGroupBuilder MapGoodsEndpoints(this WebApplication app, string authRouteBase = "/api/goods")
        {
            var builder = app.MapGroup(authRouteBase);

            // Get one by id
            builder.MapGet("/{id}", async (int id, bool? withAmount, IGoodsService goods) =>
            {
                var res = await goods.GetProductAsync(id, withAmount ?? false);
                return res is not null
                    ? Results.Ok((ProductDTO)res)
                    : Results.NotFound();
            })
                .Produces<ProductDTO>()
                .WithName("get product by id");

            // Get with filtring
            builder.MapGet("/", QueryHandler(async (GoodsQueryParamsDTO queryParams, IGoodsService goods) =>
            {
                var notPaginatedRes = await goods.GetGoodsAsync(queryParams);
                return Results.Json(notPaginatedRes.Select(p => (ProductDTO)p), statusCode: StatusCodes.Status200OK);
            }))
                .Produces<IEnumerable<ProductDTO>>()
                .WithName("get goods with filter");

            // Get paginated with filtring
            builder.MapGet("/paginated", QueryHandler(async (GoodsQueryParamsDTO queryParams, IGoodsService goods) =>
            {
                var paginatedRes = await goods.GetPaginatedGoodsAsync(queryParams);
                return Results.Json(new PaginatedList<ProductDTO>(paginatedRes.Items.Select(p => (ProductDTO)p), paginatedRes.TotalItemsCount, paginatedRes.PageIndex, paginatedRes.PageSize), statusCode: StatusCodes.Status200OK);
            }))
                .Produces<PaginatedList<ProductDTO>>()
                .WithName("get paginated goods with filter");

            // Get all
            builder.MapGet("/getall", async (IGoodsService goods) => (await goods.GetGoodsAsync()).Select(p => (ProductDTO)p))
                .Produces<IEnumerable<ProductDTO>>()
                .WithName("get all goods");

            // Create one new
            builder.MapPost("/create", async (ProductDataDTO product, IGoodsService goods) =>
            {
                var productModel = new Product()
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = (await goods.GetCategoriesAsync()).Find(c => c.Id == product.CategoryId),
                    Inventory = new ProductInventory() { Amount = 100 }
                };

                bool res = await goods.AddProductAsync(productModel);

                var productDTO = (ProductDTO)productModel;

                return Results.Json(data: productDTO, statusCode: res ? StatusCodes.Status201Created : StatusCodes.Status400BadRequest);
            })
                .Produces<ProductDTO>(statusCode: StatusCodes.Status201Created)
                .WithName("post product");

            builder
                .WithTags("goods")
                .WithOpenApi();

            return builder;
        }

        private static Delegate QueryHandler<T_Service>(Func<GoodsQueryParamsDTO, T_Service, Task<IResult>> handler) where T_Service : class =>
           async (T_Service service, string? NameSearch, int? CategoryId, int[]? CategoriesList, decimal? MinPrice, decimal? MaxPrice, string? OrderBy, bool? OrderByDescending, int? PageIndex, int? PageSize, bool? WithAmount) =>
               await handler(
                   new(
                       NameSearch: NameSearch,
                       Categories: new(CategoryId, CategoriesList),
                       Price: new(MinPrice, MaxPrice),
                       Ordering: new(OrderBy, OrderByDescending ?? false),
                       Pagination: new(PageIndex, PageSize),
                       WithAmount: WithAmount ?? false
                   ),
                   service
               );
    }
}