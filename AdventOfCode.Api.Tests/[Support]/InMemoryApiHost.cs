using TestHelpers.DotNetCore.WebApi;

namespace AdventOfCode.Api.Tests;

public class InMemoryApiHost(WebApiTesterConfiguration configuration) 
    : WebApiTestHelper<Program>(configuration)  
{
    public AdventOfCodeApiCallAdapter Adapter => configuration.Adapter;

}
