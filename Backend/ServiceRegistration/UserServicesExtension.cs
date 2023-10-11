using Backend.Services.Users;
using Backend.Services.Users.Interfaces;

namespace Backend.ServiceRegistration
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