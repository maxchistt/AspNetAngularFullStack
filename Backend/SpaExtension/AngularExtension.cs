using Backend.SpaConfig;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace Backend.SpaExtension
{
    public static class AngularExtension
    {

        public static void AddDevExceptionPage(this WebApplication app)
        {
            if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();
        }

        public static void AddAngularSpa(this WebApplication app, string? proxy = null)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = AngularConfig.Source;
                if (app.Environment.IsDevelopment())
                {
                    
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