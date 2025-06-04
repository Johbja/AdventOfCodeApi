using TestHelpers.DotNetCore.WebApi;

namespace AdventOfCode.Testing;
public class AdventOfCodeTestAdapter(AdventOfCodeApiCallAdapter apiCallAdapter) : IApiCallHelper
{
    private readonly AdventOfCodeApiCallAdapter _apiCallAdapter = apiCallAdapter;

    public Task<AssertableHttpResponse> DeleteAsync(
        string requestUri, 
        bool ensureSuccessStatusCode = true, 
        Action<HttpClient> preRequestConfigureHttpClientAction = null)
    {
        return _apiCallAdapter.DeleteAsync(
            requestUri, 
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


    public void Dispose(){}
}

