using Backend.Shared;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Backend.TestEnpoints
{
    public static class TestEndpointsExtension
    {
        public static RouteGroupBuilder MapTestEndpoints(this WebApplication app, string authRouteBase = "/api")
        {
            var builder = app.MapGroup(authRouteBase);

            builder.MapPost("/posttest", [Authorize] (TestData data) =>
            {
                return JsonConvert.SerializeObject(data);
            })
                .WithName("posttest");

            builder.MapPost("/posttestform", [Authorize] (HttpContext ctx) =>
            {
                /*var ind = ctx.Request.Form["Id"];
                var name = ctx.Request.Form["Name"];
                return JsonConvert.SerializeObject(new TestData() { Id = ind, Name = name });*/
                return JsonConvert.SerializeObject(FormMapper.Map<TestData>(ctx.Request.Form));
            })
                .WithName("posttestform");

            builder
                .WithTags("test")
                .WithOpenApi();

            return builder;
        }
    }
}