using API.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace API.Extensions;

public static class SwaggerServicesExtensions
{
    public static IServiceCollection AddSwaggerExtensions(this IServiceCollection services, IConfiguration c)
    {
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("NumberOrderingAPI_v1", new OpenApiInfo { Title = "Number Ordering API", Version = "v1" });

            var apiDocumentationPath = Path.Combine(AppContext.BaseDirectory, "API.xml");
            config.IncludeXmlComments(apiDocumentationPath);

            var businessLayerDocumentationPath = Path.Combine(AppContext.BaseDirectory, "BusinessLayer.xml");
            config.IncludeXmlComments(businessLayerDocumentationPath);
        });

        services.AddSwaggerExamplesFromAssemblies(typeof(NumberController).Assembly);

        return services;
    }
}