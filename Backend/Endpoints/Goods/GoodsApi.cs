using Backend.DTOs.Goods;
using Backend.DTOs.GoodsFiltering;
using Backend.Models.Goods;
using Backend.Services.DAL.Interfaces;
using Backend.Shared.Other;

namespace Backend.Endpoints.Goods
{
    public static class GoodsApi
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
            builder.MapGet("/", async ([AsParameters] GoodsQueryParamsRequestDTO queryParams, IGoodsService goods) =>
            {
                var notPaginatedRes = await goods.GetGoodsAsync((GoodsQueryParamsDTO)queryParams);
                return Results.Json(notPaginatedRes.Select(p => (ProductDTO)p), statusCode: StatusCodes.Status200OK);
            })
                .Produces<IEnumerable<ProductDTO>>()
                .WithName("get goods with filter");

            // Get paginated with filtring
            builder.MapGet("/paginated", async ([AsParameters] GoodsQueryParamsRequestDTO queryParams, IGoodsService goods) =>
            {
                var paginatedRes = await goods.GetPaginatedGoodsAsync((GoodsQueryParamsDTO)queryParams);
                return Results.Json(new PaginatedList<ProductDTO>(paginatedRes.Items.Select(p => (ProductDTO)p), paginatedRes.TotalItemsCount, paginatedRes.PageIndex, paginatedRes.PageSize), statusCode: StatusCodes.Status200OK);
            })
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
    }
}