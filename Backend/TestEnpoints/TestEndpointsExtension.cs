using Backend.Shared;
using Microsoft.AspNetCore.Authorization;

namespace Backend.TestEnpoints
{
    public static class TestEndpointsExtension
    {
        public static RouteGroupBuilder MapTestEndpoints(this WebApplication app, string authRouteBase = "/api")
        {
            var builder = app.MapGroup(authRouteBase);

            builder.MapPost("/posttest", [Authorize] (TestDataDTO data) =>
            {
                return data;
            })
                .WithName("posttest");

            builder.MapPost("/posttestform", [Authorize] (HttpRequest request) =>
            {
                return FormMapper.Map<TestDataWithFileFormDTO>(request.Form);
            })
                .Accepts<TestDataWithFileFormDTO>("multipart/form-data")
                .WithName("posttestform");

            builder
                .WithTags("test")
                .WithOpenApi();

            return builder;
        }
    }
}