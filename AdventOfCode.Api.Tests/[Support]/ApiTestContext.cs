using Microsoft.Extensions.DependencyInjection;
using TestHelpers.DotNetCore.WebApi;

namespace AdventOfCode.Api.Tests;

[CollectionDefinition(Name)]
public class ApiTestCollection : ICollectionFixture<ApiTestContext>
{
    public const string Name = "Advent of code hosting collection";
}

public class ApiTestContext : IDisposable
{
    private readonly Lazy<InMemoryApiHost> _inMemoryHost;
    
    public ApiTestContext()
    {
        _inMemoryHost = new Lazy<InMemoryApiHost>(() => new InMemoryApiHost(new WebApiTesterConfiguration()));
    }

    public AdventOfCodeApiCallAdapter ApiCallAdapter => _inMemoryHost.Value.Adapter;

    public void Dispose()
    {
        if(_inMemoryHost.IsValueCreated)
            _inMemoryHost.Value.Dispose();
    }
}

public class WebApiTesterConfiguration(IReadOnlyCollection<Header> defaultHeaders = null) 
    : WebApiTestHelperConfiguration(Console.WriteLine)
{
    private readonly IReadOnlyCollection<Header> _defaultHeaders = defaultHeaders;
    internal AdventOfCodeApiCallAdapter Adapter { get; private set; }

    public override IApiCallHelper CreateApiCallHelper(HttpClient httpClient)
    {
        Adapter = new AdventOfCodeApiCallAdapter(httpClient, _defaultHeaders);

        return Adapter;
    }

    public override void ConfigureServiceCollection(IServiceCollection services)
    {

    }
}
