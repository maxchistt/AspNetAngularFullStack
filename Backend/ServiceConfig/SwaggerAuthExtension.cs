using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Backend.ServiceConfig
{
    public static class SwaggerAuthExtension
    {
        public static void AddAuth(this SwaggerGenOptions option)
        {
            var authType = "Bearer JWT";

            var requirement = new OpenApiSecurityRequirement
            {{
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = authType
                    }
                },
                new string[]{}
            }};

            var scheme = new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            };

            option.AddSecurityDefinition(authType, scheme);
            option.AddSecurityRequirement(requirement);
        }

        public static void AddSwaggerGenWithJWTAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt => opt.AddAuth());
        }
    }
}