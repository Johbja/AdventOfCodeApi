using AdventOfCode.Application.Extensions;
using AdventOfCode.Application.Json;
using AdventOfCode.Application.Operations;
using AdventOfCode.Application.Operations.Days;
using AdventOfCode.Application.Operations.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdventOfCode.Application.Tests.Fixtures;

public class AdventOfCodeOperationsFixture : IDisposable
{
    public IServiceProvider ServiceProvider { get; set; }

    public AdventOfCodeOperationsFixture()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddTransient<IApplicationOperationManager, ApplicationOperationManager>();
                services.AddTransient<IApplicationOperationResolver, ApplicationOperationResolver>();
                services.AddSingleton<IJsonSerializer, MicrosoftJsonSerializer>();

                var applicationOperations = typeof(IApplicationOperationAssemblyMarker)
                    .GetAssemblyOfMarker()
                    .GetImplementationsOfInterface(typeof(IApplicationOperation), isTest:true);

                foreach (var operation in applicationOperations)
                {
                    services.AddTransient(operation);
                }
            })
            .Build();

        ServiceProvider = host.Services;
    }

    public void Dispose()
    {
        (ServiceProvider as IDisposable)?.Dispose();
    }
}

