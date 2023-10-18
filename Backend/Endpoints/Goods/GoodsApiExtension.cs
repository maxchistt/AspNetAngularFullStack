using Backend.DTOs.Goods;
using Backend.DTOs.GoodsFiltering;
using Backend.Services.DAL.Interfaces;

namespace Backend.Endpoints.Goods
{
    public static class GoodsApiExtension
    {
        public static RouteGroupBuilder MapGoodsEndpoints(this WebApplication app, string authRouteBase = "/api/goods")
        {
            var builder = app.MapGroup(authRouteBase);

            builder.MapGet("/goods/{id}", async (int id, IGoodsService goods) =>
            {
                var res = await goods.GetProductAsync(id);
                return res is not null
                    ? Results.Ok((ProductWithAmountDTO)res)
                    : Results.NotFound();
            })
                .Produces<ProductWithAmountDTO>()
                .WithName("get product by id");

            builder.MapGet("/getallgoods", async (IGoodsService goods) => (await goods.GetGoodsAsync()).Select(p => (ProductWithAmountDTO)p))
                .Produces<IEnumerable<ProductWithAmountDTO>>()
                .WithName("get all goods");

            builder.MapGet("/goods", async (
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

                var res = await goods.GetGoodsAsync(filter);
                return Results.Json(res.Select(p => (ProductWithAmountDTO)p), statusCode: StatusCodes.Status200OK);
            })
                .Produces<IEnumerable<ProductWithAmountDTO>>()
                .WithName("get goods with filter");

            builder.MapGet("/categories", async (IGoodsService goods) => (await goods.GetCategoriesAsync()).Select(c => (CategoryDTO)c))
                .WithName("get categories");

            builder.MapPost("/postproduct", async (ProductDataDTO product, IGoodsService goods) =>
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

            builder.MapPost("/postcategory", async (CategoryDataDTO category, IGoodsService goods) =>
            {
                var categoryModel = new Models.Goods.Category()
                {
                    Name = category.Name
                };

                bool res = await goods.AddCategoryAsync(categoryModel);

                var categoryDTO = (CategoryDTO)categoryModel;

                return Results.Json(data: categoryDTO, statusCode: res ? StatusCodes.Status201Created : StatusCodes.Status400BadRequest);
            })
                .Produces<CategoryDTO>(statusCode: StatusCodes.Status201Created)
                .WithName("post category");

            builder
                .WithTags("goods")
                .WithOpenApi();

            return builder;
        }
    }
}