using AdventOfCode.Application.RestServices.Interfaces;

namespace AdventOfCode.Application.RestServices;

public class RestServiceSettings : IRestServiceSettings
{
    //TODO: impliment this more if enviroment controll is ever needed

    public string BaseClientUrl => throw new NotImplementedException();

    private readonly List<Header> _defaultHeaders = new();
    private readonly HttpClientHandler _httpClientHandler = new();

    public IReadOnlyCollection<Header> DefaultHeaders => _defaultHeaders;

    protected void AddDefaultHeader(Header header)
    {
        _defaultHeaders.Add(header);
    }

    protected void ConfigureHttpClientHandler(Action<HttpClientHandler> settings)
    {
        settings.Invoke(_httpClientHandler);
    }
}
