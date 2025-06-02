namespace AdventOfCode.Application.RestServices.ApiCalls;

public abstract class RestApiCallWithOutput<TOutput> : RestApiCallBase<Null, TOutput>
    where TOutput : class
{
    protected abstract HttpRequestMessage BuildRequest(RequestBuilder requestbuilder);
    protected override HttpRequestMessage BuildRequestInternal(RequestBuilder builder)
    {
        return BuildRequest(builder);
    }
}

