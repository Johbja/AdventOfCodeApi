using AdventOfCode.Application.Operations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using AdventOfCode.Application.Operations.Interfaces;
using AdventOfCode.Application.RestServices;
using AdventOfCode.Application.Datasources.AdventOfCode;
using AdventOfCode.Application.Json;

namespace AdventOfCode.Application.Extensions;

public static class ServiceProviderExtensions
{
    public static void AddApplicationDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationOperations();
        builder.Services.AddApplicationServices();
        builder.Services.AddApplicationHttpClients();
        builder.Services.AddApplicationSettings(); 
    }

    private static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IApplicationOperationManager, ApplicationOperationManager>();
        services.AddTransient<IApplicationOperationResolver, ApplicationOperationResolver>();
        services.AddSingleton<IRestClientFactory, RestClientFactory>();
        services.AddSingleton<AdventOfCodeRestService>();
        services.AddSingleton<IJsonSerializer, MicrosoftJsonSerializer>();
    }

    private static void AddApplicationOperations(this IServiceCollection services)
    {
        var applicationOperations = typeof(IApplicationOperationAssemblyMarker)
            .GetAssemblyOfMarker()
            .GetImplementationsOfInterface(typeof(IApplicationOperation));

        foreach (var operation in applicationOperations) 
        {
            services.AddTransient(operation);
        }
    }

    private static void AddApplicationHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient(RestCallDefinitions.AdventOfCode)
            .ConfigurePrimaryHttpMessageHandler(() => RestCallDefinitions.AdventOfCodeClientHandler);

        services.Configure<IISServerOptions>(options =>
        {
            options.MaxRequestBodySize = 100000000;
        });
    }

    private static void AddApplicationSettings(this IServiceCollection services)
    {
        services.AddSingleton<IAdventOfCodeServiceSettings, AdventOfCodeServiceSettings>();
    }

}
