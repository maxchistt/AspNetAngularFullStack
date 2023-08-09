using Backend.SpaConfig;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace Backend.SpaExtension
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

        public static void AddAngularSpa(this WebApplication app, bool useDevSpaOnDev = false, string? proxy = null)
        {
            app.UseSpa(spa =>
            {
                
                if (useDevSpaOnDev && app.Environment.IsDevelopment())
                {
                    spa.Options.SourcePath = AngularConfig.Source;
                    if (string.IsNullOrEmpty(proxy))
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                    else
                    {
                        //"http://localhost:4000"
                        spa.UseProxyToSpaDevelopmentServer(proxy);
                    }
                }
            });
        }

        public static void ConfigAngularStaticFiles(this IServiceCollection services)
        {
            services.AddSpaStaticFiles(cfg => cfg.RootPath = AngularConfig.SpaStaticRoot);
        }
    }
}