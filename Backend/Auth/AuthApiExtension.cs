namespace Backend.Auth
{
    public static class AuthApiExtension
    {
        public static RouteGroupBuilder MapAuthEndpoints(this WebApplication app, string authRouteBase = "/api/auth")
        {
            var builder = app.MapGroup(authRouteBase);

            builder.MapPost("/login", AuthEndpointHandlers.Login)
               .Produces<string>()
               .WithName("auth login")
               .WithDescription("login endpoint");

            builder.MapPost("/register", AuthEndpointHandlers.Register)
                .Produces(statusCode: StatusCodes.Status201Created)
                .WithName("auth register")
                .WithDescription("register endpoint");

            builder.MapPost("/changepassword", AuthEndpointHandlers.PasswordChange)
                .WithName("auth change password")
                .WithDescription("password change endpoint");

            builder
                .WithTags("auth")
                .WithOpenApi();

            return builder;
        }
    }
}