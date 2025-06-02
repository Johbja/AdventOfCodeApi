using AdventOfCode.Application.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Application.RestServices;

public interface IRestClient
{
    string BaseUrl { get; }
    IJsonSerializer JsonSerializer { get; }
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, Type callerType, int timeout);
}

public class RestClient(
    HttpClient httpClient,
    IJsonSerializer jsonSerializer) 
    : IRestClient
{
    private readonly HttpClient _httpClient = httpClient;

    public string BaseUrl => _httpClient.BaseAddress.ToString();
    public IJsonSerializer JsonSerializer { get; } = jsonSerializer;

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, Type callerType, int timeout)
    {
        using var token = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));
        try
        {
            return await _httpClient.SendAsync(request, token.Token);
        }
        catch (OperationCanceledException ex)
        {
            throw CreateNewExceptionForCase(ex);
        }
    }

    private Exception CreateNewExceptionForCase(OperationCanceledException ex)
    {
        //TODO: add more exception support
        return new Exception("Http call timed out", ex);
    }
}
