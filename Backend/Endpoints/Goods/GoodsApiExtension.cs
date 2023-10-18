using Backend.DTOs.Goods;
using Backend.DTOs.GoodsFiltering;
using Backend.Services.DAL.Interfaces;
using Backend.Shared.Other;

namespace Backend.Endpoints.Goods
{
    public static class GoodsApiExtension
    {
        public static RouteGroupBuilder MapGoodsEndpoints(this WebApplication app, string authRouteBase = "/api/goods")
        {
            var builder = app.MapGroup(authRouteBase);

            builder.MapGet("/{id}", async (int id, IGoodsService goods) =>
            {
                var res = await goods.GetProductAsync(id);
                return res is not null
                    ? Results.Ok((ProductWithAmountDTO)res)
                    : Results.NotFound();
            })
                .Produces<ProductWithAmountDTO>()
                .WithName("get product by id");

            builder.MapGet("/", async (
                string? NameSearch,
                int? CategoryId,
                int[]? CategoriesList,
                decimal? MinPrice,
                decimal? MaxPrice,
                string? OrderBy,
                bool? OrderByDescending,
                int? PageIndex,
                int? PageSize,
                bool? WithAmount,
                IGoodsService goods
            ) =>
            {
                GoodsQueryParamsDTO filter = new(
                    NameSearch: NameSearch,
                    Categories: new(CategoryId, CategoriesList),
                    Price: new(MinPrice, MaxPrice),
                    Ordering: new(OrderBy, OrderByDescending ?? false),
                    Pagination: new(PageIndex, PageSize),
                    WithAmount: WithAmount ?? false
                );

                var notPaginatedRes = await goods.GetGoodsAsync(filter);
                return Results.Json(notPaginatedRes.Select(p => (ProductWithAmountDTO)p), statusCode: StatusCodes.Status200OK);
            })
                .Produces<IEnumerable<ProductWithAmountDTO>>()
                .WithName("get goods with filter");

            builder.MapGet("/paginated", async (
                string? NameSearch,
                int? CategoryId,
                int[]? CategoriesList,
                decimal? MinPrice,
                decimal? MaxPrice,
                string? OrderBy,
                bool? OrderByDescending,
                int? PageIndex,
                int? PageSize,
                bool? WithAmount,
                IGoodsService goods
            ) =>
            {
                GoodsQueryParamsDTO filter = new(
                    NameSearch: NameSearch,
                    Categories: new(CategoryId, CategoriesList),
                    Price: new(MinPrice, MaxPrice),
                    Ordering: new(OrderBy, OrderByDescending ?? false),
                    Pagination: new(PageIndex, PageSize),
                    WithAmount: WithAmount ?? false
                );

                var paginatedRes = await goods.GetPaginatedGoodsAsync(filter);
                return Results.Json(new PaginatedList<ProductWithAmountDTO>(paginatedRes.Items.Select(p => (ProductWithAmountDTO)p), paginatedRes.TotalItemsCount, paginatedRes.PageIndex, paginatedRes.PageSize), statusCode: StatusCodes.Status200OK);
            })
                .Produces<PaginatedList<ProductWithAmountDTO>>()
                .WithName("get paginated goods with filter");

            builder.MapGet("/getall", async (IGoodsService goods) => (await goods.GetGoodsAsync()).Select(p => (ProductWithAmountDTO)p))
                .Produces<IEnumerable<ProductWithAmountDTO>>()
                .WithName("get all goods");

            builder.MapPost("/create", async (ProductDataDTO product, IGoodsService goods) =>
            {
                var productModel = new Models.Goods.Product()
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = (await goods.GetCategoriesAsync()).Find(c => c.Id == product.CategoryId),
                    Inventory = new Models.Goods.ProductInventory() { Amount = 100 }
                };

                bool res = await goods.AddProductAsync(productModel);

                var productDTO = (ProductWithAmountDTO)productModel;

                return Results.Json(data: productDTO, statusCode: res ? StatusCodes.Status201Created : StatusCodes.Status400BadRequest);
            })
                .Produces<ProductWithAmountDTO>(statusCode: StatusCodes.Status201Created)
                .WithName("post product");

            builder
                .WithTags("goods")
                .WithOpenApi();

            return builder;
        }
    }
}