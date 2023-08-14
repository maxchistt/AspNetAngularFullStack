using System.Text.Json;

namespace Backend.AngularSpa
{
    public static class SpaInfoEndpointExt
    {
        public static RouteHandlerBuilder MapSpaInfo(this WebApplication app, string route = "/SpaDir")
        {
            return app.MapGet(route, () =>
            {
                return JsonSerializer.Serialize(AngularConfig.SpaStaticRoot);
            });
        }
    }
}