using AdventOfCode.Application.Extensions;
using Json = AdventOfCode.Application.Json;
using System.Net;
using TestHelpers.DotNetCore.WebApi;
using Microsoft.AspNetCore.Http;

namespace AdventOfCode.Api.Tests;

public class AdventOfCodeApiCallAdapter : IApiCallHelper
{
    private readonly HttpClient _httpClient;
    private readonly Json.IJsonSerializer _jsonSerializer = new JsonSerializerForApiTests();

    public AdventOfCodeApiCallAdapter(
        HttpClient httpClient,
        IReadOnlyCollection<Header> defaultHeaders)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        
        if(defaultHeaders != null)
        {
            foreach (var header in defaultHeaders)
            {
                _httpClient.DefaultRequestHeaders.Add(header.Name, header.Value);
            }
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

    public Task<AssertableHttpResponse> PostAsJsonAsync<T>(
        string requestUri, 
        T value, 
        bool ensureSuccessStatusCode = true, 
        Action<HttpClient> preRequestConfigureHttpClientAction = null)
    {
        preRequestConfigureHttpClientAction?.Invoke(_httpClient);

        return SendAsync(
            CreateRequestMessage(
                HttpMethod.Post,
                requestUri,
                CreateJsonContent(value)),
            ensureSuccessStatusCode);
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

    private HttpRequestMessage CreateRequestMessage(
       HttpMethod method,
       string url,
       HttpContent content = null)
    {
        var request = new HttpRequestMessage(method, url);

        if (content != null)
        {
            request.Content = content;
        }

        return request;
    }

    private async Task<AssertableHttpResponse> SendAsync(
       HttpRequestMessage request,
       bool ensureSuccessStatusCode)
    {
        var response = await _httpClient.SendAsync(request);
        var body = await TryGetContentAsString(response.Content);
        var wrappedStatusCode = (int)HttpStatusCode.NoContent;

        string errorMessage = null;
        if (ensureSuccessStatusCode && !response.IsSuccessStatusCode)
        {
            errorMessage = $"Outer response status code {response.StatusCode} is not considered as success.{Environment.NewLine}Body: {body}{Environment.NewLine}";
        }

        if (errorMessage.HasValue())
            throw new Exception(errorMessage);

        var assertableResponse = new AssertableHttpResponse(
            response.StatusCode,
            body,
            response.Headers);

        return assertableResponse;
    }

    private async Task<string> TryGetContentAsString(HttpContent content)
    {
        if (content == null)
            return "No content (null)";

        var contentString = await content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(contentString))
            return "No content (empty)";

        return contentString;
    }

    private HttpContent CreateJsonContent(object body)
    {
        if(body is IFormFile file)
        {
            var form = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream());

            //TODO: get paramameter name from enpoint instead of hardcoded value
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");
            form.Add(fileContent, "payload", file.FileName ?? "file");

            return form;    
        }

        if (body is string bodyString)
            return new JsonContent(bodyString);

        return new JsonContent(_jsonSerializer.ToJsonString(body));
    }

    private bool LooksLikeItContainsJson(string value) => !string.IsNullOrEmpty(value) && (value.StartsWith("{") || value.StartsWith("["));
    private bool LooksLikeAWrappedResponse(string value) => !string.IsNullOrWhiteSpace(value) && value.Contains("trackingId");
    private bool IsSuccessStatusCode(int statusCode) => statusCode >= 200 && statusCode < 300;

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
