using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace Backend.AngularSpa
{
    public static class AngularExtension
    {
        public static void AddAngularSpaStatic(this WebApplication app, bool alwaysUseStatic = false)
        {
            if (!app.Environment.IsDevelopment() || alwaysUseStatic)
            {
                app.UseSpaStaticFiles();
            }
        }

        public static void AddAngularSpa(this WebApplication app, bool useDevSpaOnDev = true)
        {
            app.UseSpa(spa =>
            {
                if (useDevSpaOnDev && app.Environment.IsDevelopment())
                {
                    AngularCmd.StartAngularDev(Directory.GetCurrentDirectory() + "\\" + AngularConfig.Source);
                }
            });
        }

        public static void ConfigAngularStaticFiles(this IServiceCollection services)
        {
            services.AddSpaStaticFiles(cfg => cfg.RootPath = AngularConfig.SpaStaticRoot);
        }
    }
}