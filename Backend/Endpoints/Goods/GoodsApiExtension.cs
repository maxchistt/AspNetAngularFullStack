using Backend.DTOs.Goods;
using Backend.DTOs.Other;
using Backend.Services.DAL.Interfaces;

namespace Backend.Endpoints.Goods
{
    public static class GoodsApiExtension
    {
        public static RouteGroupBuilder MapGoodsEndpoints(this WebApplication app, string authRouteBase = "/api/goods")
        {
            var builder = app.MapGroup(authRouteBase);

            builder.MapGet("/getgoods", async (IGoodsService goods) => await goods.GetGoodsAsync())
               .WithName("get goods");

            builder.MapPost("/getgoods", async (GoodsFilteringParamsDTO filter, IGoodsService goods) => await goods.GetPaginatedGoodsAsync(filter))
               .WithName("get goods with filter");

            builder.MapGet("/getcategories", async (IGoodsService goods) => await goods.GetCategoriesAsync())
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

                ProductDTO productDTO = new ProductDTO() { Id=productModel.Id,Price=Pr };

                return Results.Json(data: productModel, statusCode: res ? StatusCodes.Status201Created : StatusCodes.Status400BadRequest);
            })
              .WithName("post product");

            builder.MapPost("/postcategory", async (CategoryDataDTO category, IGoodsService goods) =>
            {
                var categoryModel = new Models.Goods.Category()
                {
                    Name = category.Name
                };

                bool res = await goods.AddCategoryAsync(categoryModel);

                return Results.Json(data: categoryModel, statusCode: res ? StatusCodes.Status201Created : StatusCodes.Status400BadRequest);
            })
              .WithName("post category");

            builder
                .WithTags("goods")
                .WithOpenApi();

            return builder;
        }
    }
}