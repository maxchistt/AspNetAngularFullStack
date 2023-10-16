using Backend.Services.DAL;
using Backend.Services.DAL.Interfaces;
using Backend.Services.Other;
using Backend.Services.Other.Interfaces;

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