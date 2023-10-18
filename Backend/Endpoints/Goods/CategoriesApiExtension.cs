using Backend.DTOs.Goods;
using Backend.Services.DAL.Interfaces;
using Backend.Shared.Other;

namespace Backend.Endpoints.Goods
{
    public static class CategoriesApiExtension
    {
        public static RouteGroupBuilder MapCategoriesEndpoints(this WebApplication app, string authRouteBase = "/api/categories")
        {
            var builder = app.MapGroup(authRouteBase);

            builder.MapGet("/", async (IGoodsService goods) => (await goods.GetCategoriesAsync()).Select(c => (CategoryDTO)c))
                .WithName("get categories");

            builder.MapPost("/create", async (CategoryDataDTO category, IGoodsService goods) =>
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
                .WithName("create category");

            builder
                .WithTags("categories")
                .WithOpenApi();

            return builder;
        }
    }
}
