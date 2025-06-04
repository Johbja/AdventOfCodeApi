using AdventOfCode.Application.Extensions;
using TestHelpers.DotNetCore.WebApi;

namespace AdventOfCode.Api.Tests;
public class AdventOfCodeTestAdapter(AdventOfCodeApiCallAdapter apiCallAdapter, string baseAddress = "") : IApiCallHelper
{
    private readonly AdventOfCodeApiCallAdapter _apiCallAdapter = apiCallAdapter;
    private readonly string _baseAddress = baseAddress;

    public Task<AssertableHttpResponse> DeleteAsync(
        string requestUri, 
        bool ensureSuccessStatusCode = true, 
        Action<HttpClient> preRequestConfigureHttpClientAction = null)
    {
        return _apiCallAdapter.DeleteAsync(
            AddBaseAddressToRequest(requestUri), 
            ensureSuccessStatusCode, 
            preRequestConfigureHttpClientAction);
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
        return _apiCallAdapter.PostAsJsonAsync(
            AddBaseAddressToRequest(requestUri),
            value,
            ensureSuccessStatusCode,
            preRequestConfigureHttpClientAction);
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


    private string AddBaseAddressToRequest(string requestUri)
    {
        if (_baseAddress.HasValue())
        {
            return $"{_baseAddress}/{requestUri}";
        }

        return requestUri;
    }

    public void Dispose(){}
}

