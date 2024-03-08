using API.Controllers;
using Core;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using BusinessLayer.DependencyInjections;
using System.Text.Json.Serialization;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddEndpointsApiExplorer()
                .AddSwagger()
                .AddFileIOManager(config);

        services.AddBusinessServices();

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
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

    public static IServiceCollection AddFileIOManager(this IServiceCollection services, IConfiguration config)
    {
        var filePath = config.GetValue<string>("NumberRepositorySettings:filePath");
        var fileName = config.GetValue<string>("NumberRepositorySettings:fileName");

        if (string.IsNullOrWhiteSpace(filePath))
        {
            filePath = Directory.GetParent(Environment.CurrentDirectory)!.FullName;
        }

        if (string.IsNullOrWhiteSpace(fileName))
        {
            fileName = "numbers.txt";
        }

        var fileIOManager = new FileIOManager(filePath, fileName);

        services.AddSingleton<IFileIOManager>(fileIOManager);

        return services;
    }
}