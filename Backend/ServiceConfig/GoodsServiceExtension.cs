using Backend.Services.DAL;
using Backend.Services.DAL.Interfaces;

namespace Backend.ServiceConfig
{
    public static class GoodsServiceExtension
    {
        public static void AddGoodsService(this IServiceCollection services)
        {
            services.AddScoped<IGoodsService, GoodsService>();
        }
    }
}