using Microsoft.AspNetCore.SpaServices;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

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
                    //spa.Options.DevServerPort = 4200;
                    spa.Options.SourcePath = AngularConfig.Source;

                    spa.UseAngularDevServer();

                    //spa.UseAngularCliServer("start");
                    //spa.UseProxyToSpaDevelopmentServer($"http://127.0.0.1:{spa.Options.DevServerPort}/");
                }
            });

        }

        public static void UseAngularDevServer(this ISpaBuilder spa)
        {
            
            var workdir = Directory.GetCurrentDirectory() + "\\" + spa.Options.SourcePath;
            var appLifetime = spa.ApplicationBuilder.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            new AngularDevServer(workdir, appLifetime).Start();
            //spa.UseProxyToSpaDevelopmentServer($"http://127.0.0.1:{spa.Options.DevServerPort}/");
        }

        public static void ConfigAngularStaticFiles(this IServiceCollection services)
        {
            services.AddSpaStaticFiles(cfg => cfg.RootPath = AngularConfig.SpaStaticRoot);
        }
    }
}