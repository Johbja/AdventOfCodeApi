using AdventOfCode.Application.Extensions;
using AdventOfCode.Application.Json;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace AdventOfCode.Application.RestServices.ApiCalls;
public class RestApiCallBase<TInput, TOutput> : IRestApiCall
    where TInput : class
    where TOutput : class
{
    public const int DefaultTimeout = 100;
    protected static bool IsStreamOutput => typeof(TOutput) == typeof(Stream);
    private static bool _ignoreInput => typeof(TInput) == typeof(Null);
    private static bool _ignoreOutput => typeof(TOutput) == typeof(Null);
    
    private IRestClient _restClient;

    public RequestBuilder RequestBuilder { get; private set; }

    public void Initialize(IRestClient restClient)
    {
        _restClient = restClient;
        RequestBuilder = new RequestBuilder(restClient.JsonSerializer );
    }

    protected virtual HttpRequestMessage BuildRequestInternal(TInput input, RequestBuilder builder)
    {
        throw new NotImplementedException($"{nameof(BuildRequestInternal)} not implemented in child class");
    }
    protected virtual HttpRequestMessage BuildRequestInternal(RequestBuilder builder)
    {
        throw new NotImplementedException($"{nameof(BuildRequestInternal)} not implemented in child class");
    }

    public virtual ITraceableOutput<Task<TOutput>> Execute(
        TInput input = null, 
        int timeout = DefaultTimeout)
    {
        var stopwatch = new Stopwatch();

        try
        {
            var output = new TraceableOutput<Task<TOutput>>(stopwatch, WrapCallForTraceableOutput(input, timeout, stopwatch));
            return output;
        }
        catch
        {
            //TODO: handel exemptions
            throw;
        }
    }

    private async Task<TOutput> WrapCallForTraceableOutput(TInput input, int timeout, Stopwatch stopwatch)
    {
        var response = await ExecuteInternal(input, timeout, stopwatch);
        var stringContent = await response.Content.TryGetContentAsString(IsStreamOutput);
        var result = await HandleSuccessResponse(response.StatusCode, stringContent, response)!;
        
        return result;
    }

    private async Task<HttpResponseMessage> ExecuteInternal(TInput input, int timeout, Stopwatch stopwatch)
    {
        stopwatch.Start();
        var request = _ignoreInput ? BuildRequestInternal(RequestBuilder) : BuildRequestInternal(input, RequestBuilder);
        
        return await ExecuteRestClient(request, timeout);
    }

    protected virtual Task<HttpResponseMessage> ExecuteRestClient(HttpRequestMessage request, int timeout)
    {
        return _restClient.SendAsync(request, GetType(), timeout);
    }

    protected virtual async Task<TOutput> HandleSuccessResponse(
        HttpStatusCode httpStatusCode, 
        string responseContent, 
        HttpResponseMessage response)
    {
        if (_ignoreInput)
            return default;

        if(IsStreamOutput)
        {
            var stream = await response.Content.ReadAsStreamAsync();
            return stream as TOutput;
        }

        return HandleSuccessResponse(httpStatusCode, responseContent);
    }

    protected virtual TOutput HandleSuccessResponse(HttpStatusCode httpStatusCode, string responseContent)
    {
        var result = JsonSerializer.Deserialize<TOutput>(responseContent);
        return result;
    }
}
