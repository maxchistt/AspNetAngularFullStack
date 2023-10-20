using Backend.Services.DAL;
using Backend.Services.DAL.Interfaces;
using Backend.Services.DAL.Utilities;
using Backend.Services.DAL.Utilities.Interfaces;

namespace Backend.ServiceConfig
{
    public static class UserServicesExtension
    {
        public static void AddUserServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHashingService, PasswordHashingService>();
            services.AddScoped<IUsersService, UsersHashedService>();
        }
    }
}