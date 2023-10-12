using Backend.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.ServiceConfig
{
    public static class DbContextExtension
    {
        public static void AddSqlServerDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Database")));
        }
    }
}