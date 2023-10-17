using Backend.DTOs.Goods;
using Backend.DTOs.GoodsFiltering;
using Backend.Services.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Endpoints.Goods
{
    public static class GoodsApiExtension
    {
        public static RouteGroupBuilder MapGoodsEndpoints(this WebApplication app, string authRouteBase = "/api/goods")
        {
            var builder = app.MapGroup(authRouteBase);

            builder.MapGet("/getallgoods", async (IGoodsService goods) => (await goods.GetGoodsAsync()).Select(p => (ProductWithAmountDTO)p))
               .WithName("get all goods");

            builder.MapGet("/getgoods", async ([FromBody] GoodsFilteringParamsDTO filter, IGoodsService goods) => (await goods.GetGoodsAsync(filter)).Select(p => (ProductWithAmountDTO)p))
               .WithName("get goods with filter");

            builder.MapGet("/getcategories", async (IGoodsService goods) => (await goods.GetCategoriesAsync()).Select(c => (CategoryDTO)c))
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