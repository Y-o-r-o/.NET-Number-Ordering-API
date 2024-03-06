using BusinessLayer.BusinessServices;
using BusinessLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.DependencyInjections;

public static class BusinessServicesExtensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<INumberService, NumberService>();

        return services;
    }
}