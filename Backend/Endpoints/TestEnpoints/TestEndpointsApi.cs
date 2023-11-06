using Backend.DTOs.Other;
using Backend.Services.DAL.Interfaces;
using Backend.Shared.AuthParams;
using Backend.Shared.Other;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Backend.Endpoints.TestEnpoints
{
    public static class TestEndpointsApi
    {
        public static RouteGroupBuilder MapTestEndpoints(this WebApplication app, string authRouteBase = "/api")
        {
            var builder = app.MapGroup(authRouteBase);

            builder.MapGet("/GetCurrentUserName", [Authorize] (ClaimsPrincipal claims, IUsersService usersService) =>
            {
                if (claims.Identity is null)
                    return Results.BadRequest("No Identity");

                if (claims.Identity.Name is null)
                    return Results.BadRequest("No Identity.Name");

                var user = usersService.GetUser(claims.Identity.Name);

                if (user is null)
                    return Results.BadRequest($"No user found by \'{claims.Identity.Name}\'");

                return Results.Ok(value: $"Claim name: {claims.Identity.Name}, User Id: {user.Id}, User Name: {user.Email}");
            })
                .Produces<string>()
                .WithName("Get current user name");

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