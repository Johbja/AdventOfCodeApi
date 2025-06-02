using AdventOfCode.Application.Json;
using AdventOfCode.Application.RestServices.Interfaces;
using System.Text;

namespace AdventOfCode.Application.RestServices;

public interface IRestClientFactory
{
    IRestClient CreateRestClientWithAuth(IRestServiceWithBaseAuthSettings settings);
    IRestClient CreateRestClient(IRestServiceSettings settings);
}

public class RestClientFactory(
    IHttpClientFactory clientFactory,
    IJsonSerializer jsonSerializer)
    : IRestClientFactory
{
    private readonly IHttpClientFactory _clientFactory = clientFactory;

    public IRestClient CreateRestClient(IRestServiceSettings settings)
    {
        var httpClient = CreateHttpClient(settings);
        var restClient = CreateRestClient(httpClient);

        return restClient;
    }

    public IRestClient CreateRestClientWithAuth(IRestServiceWithBaseAuthSettings settings)
    {
        var httpClient = CreateHttpClient(settings);
        var byteArray = Encoding.ASCII.GetBytes($"{settings.Username}{settings.Password}");
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        var restClient = CreateRestClient(httpClient);
    
        return restClient;
    }

    private HttpClient CreateHttpClient(IRestServiceSettings settings)
    {
        var client = _clientFactory.CreateClient(RestCallDefinitions.AdventOfCode);
        client.BaseAddress = new Uri(settings.BaseClientUrl);

        foreach(var header in settings.DefaultHeaders)
        {
            client.DefaultRequestHeaders.Add(header.Name, header.Value);
        }

        return client;
    }

    private IRestClient CreateRestClient(HttpClient httpClient)
    {
        return new RestClient(httpClient, jsonSerializer);
    }
}
