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
                return Results.Accepted(value: data);
            })
                .Produces<TestDataDTO>(statusCode: StatusCodes.Status202Accepted)
                .WithName("posttest");

            builder.MapPost("/posttestform", [Authorize] (HttpRequest request) =>
            {
                var data = FormMapper.Map<TestDataWithFileFormDTO>(request.Form);
                return Results.Accepted(value: data);
            })
                .Accepts<TestDataWithFileFormDTO>(contentType: "multipart/form-data")
                .Produces<TestDataWithFileFormDTO>(statusCode: StatusCodes.Status202Accepted)
                .WithName("posttestform");

            builder
                .WithTags("test")
                .WithOpenApi();

            return builder;
        }
    }
}