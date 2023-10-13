using Backend.Services.DAL.Users;
using Backend.Services.DAL.Users.Interfaces;

namespace Backend.ServiceConfig
{
    public static class UserServicesExtension
    {
        public static void AddUserServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHashingService, PasswordHashingService>();
            services.AddScoped<IUserService, UserHashedService>();
        }
    }
}