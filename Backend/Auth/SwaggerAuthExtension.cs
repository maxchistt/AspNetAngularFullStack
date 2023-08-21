using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Backend.Auth
{
    public static class SwaggerAuthExtension
    {
        private static string AuthName = "Bearer JWT";

        private static OpenApiSecurityRequirement requirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id=AuthName
                    }
                },
                new string[]{}
            }
        };

        private static OpenApiSecurityScheme scheme = new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        };

        public static void UseSwaggerAndUI(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        public static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
        {
            return services.AddSwaggerGen(opt => opt.AddAuth());
        }

        public static void AddAuthWithoutRequirement(this SwaggerGenOptions option)
        {
            option.AddSecurityDefinition(AuthName, scheme);
        }

        public static void AddAuth(this SwaggerGenOptions option)
        {
            option.AddAuthWithoutRequirement();
            option.AddSecurityRequirement(requirement);
        }

        public static OpenApiOperation WithAuth(this OpenApiOperation op)
        {
            op.Security.Add(requirement);
            return op;
        }
    }
}