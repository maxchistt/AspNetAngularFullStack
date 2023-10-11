using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Backend.ServiceRegistration
{
    public static class SwaggerAuthExtension
    {
        private static readonly string _authType = "Bearer JWT";

        private static readonly OpenApiSecurityRequirement _requirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id=_authType
                    }
                },
                new string[]{}
            }
        };

        private static readonly OpenApiSecurityScheme _scheme = new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        };

        public static void AddAuth(this SwaggerGenOptions option)
        {
            option.AddSecurityDefinition(_authType, _scheme);
            option.AddSecurityRequirement(_requirement);
        }

        public static void AddSwaggerGenWithJWTAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt => opt.AddAuth());
        }
    }
}