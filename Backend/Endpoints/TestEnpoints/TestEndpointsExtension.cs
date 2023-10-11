using Backend.DTOs.Other;
using Backend.Shared.AuthParams;
using Backend.Shared.Other;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Endpoints.TestEnpoints
{
    public static class TestEndpointsExtension
    {
        public static RouteGroupBuilder MapTestEndpoints(this WebApplication app, string authRouteBase = "/api")
        {
            var builder = app.MapGroup(authRouteBase);

            builder.MapPost("/posttest", [Authorize(Roles = Roles.Combinations.AdminAndWorker)] (TestDataDTO data) =>
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
                .Accepts<TestDataWithFileFormDTO>(contentType: HttpContentTypes.MultipatFormdata)
                .Produces<TestDataWithFileFormDTO>(statusCode: StatusCodes.Status202Accepted)
                .WithName("posttestform");

            builder
                .WithTags("test")
                .WithOpenApi();

            return builder;
        }
    }
}