namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer()
                .AddSwaggerExtensions(config);

        return services;
    }
}