using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHelpers.DotNetCore.WebApi;

namespace AdventOfCode.Testing;
public class AdventOfCodeApiCallAdapter : IApiCallHelper
{
    private readonly HttpClient _httpClient;

    public AdventOfCodeApiCallAdapter(
        HttpClient httpClient, 
        IReadOnlyCollection<Header> defaultHeaders)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        
        foreach (var header in defaultHeaders)
        {
            _httpClient.DefaultRequestHeaders.Add(header.Name, header.Value);
        }
    }

    public Task<AssertableHttpResponse> DeleteAsync(string requestUri, bool ensureSuccessStatusCode = true, Action<HttpClient> preRequestConfigureHttpClientAction = null)
    {
        throw new NotImplementedException();
    }

    public Task<AssertableHttpResponse> GetAsync(string requestUri, bool ensureSuccessStatusCode = true, Action<HttpClient> preRequestConfigureHttpClientAction = null)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> GetRawAsync(string requestUri, bool ensureSuccessStatusCode = true, Action<HttpClient> preRequestConfigureHttpClientAction = null)
    {
        throw new NotImplementedException();
    }

    public Task<AssertableHttpResponse> OptionsAsync<T>(string requestUri, bool ensureSuccessStatusCode = true, Action<HttpClient> preRequestConfigureHttpClientAction = null)
    {
        throw new NotImplementedException();
    }

    public Task<AssertableHttpResponse> PostAsJsonAsync<T>(string requestUri, T value, bool ensureSuccessStatusCode = true, Action<HttpClient> preRequestConfigureHttpClientAction = null)
    {
        throw new NotImplementedException();
    }

    public Task<AssertableHttpResponse> PostFileAsync(string requestUri, string pathToFile, bool ensureSuccessStatusCode = true, Action<HttpClient> preRequestConfigureHttpClientAction = null)
    {
        throw new NotImplementedException();
    }

    public Task<AssertableHttpResponse> PutAsJsonAsync<T>(string requestUri, T value, bool ensureSuccessStatusCode = true, Action<HttpClient> preRequestConfigureHttpClientAction = null)
    {
        throw new NotImplementedException();
    }

    public Task<AssertableHttpResponse> SendAsync(HttpRequestMessage request, Action<HttpClient> preRequestConfigureHttpClientAction, bool ensureSuccessStatusCode = true)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
