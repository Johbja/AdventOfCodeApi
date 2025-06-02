namespace AdventOfCode.Application.RestServices.ApiCalls;

public abstract class RestApiCall<TInput, TOutput> 
    : RestApiCallBase<TInput, TOutput>
    where TInput : class
    where TOutput : class
{
    protected abstract HttpRequestMessage BuildRequest(TInput input, RequestBuilder requestbuilder);
    protected override HttpRequestMessage BuildRequestInternal(TInput input, RequestBuilder builder)
    {
        return BuildRequest(input, builder);
    }
}
