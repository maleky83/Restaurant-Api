using Microsoft.OpenApi;
using Restaurants.API.Middlewares;
using Serilog;

namespace Restaurants.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();

        builder.Services.AddControllers();

        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(document =>
                new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("bearerAuth", document)] = []
                });
        });

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
        builder.Services.AddScoped<ErrorHandlingMiddle>();

        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration);
        });
    }
}
