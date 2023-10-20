using Backend.Services.DAL;
using Backend.Services.DAL.Interfaces;
using Backend.Services.Utilities.DAL;
using Backend.Services.Utilities.DAL.Interfaces;

namespace Backend.ServiceConfig
{
    public static class GoodsServiceExtension
    {
        public static void AddGoodsService(this IServiceCollection services)
        {
            services.AddSingleton<IGoodsQueryFiltering, GoodsQueryFiltering>();
            services.AddSingleton<IProductOrderingExpressionParcer, ProductOrderingExpressionParcer>();
            services.AddSingleton<IGoodsQueryConfigurer, GoodsQueryConfigurer>();
            services.AddScoped<IGoodsService, GoodsService>();
        }
    }
}