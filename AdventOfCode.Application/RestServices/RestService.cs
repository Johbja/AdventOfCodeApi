using AdventOfCode.Application.Json;

namespace AdventOfCode.Application.RestServices;

public interface IRestApiCall
{
    void Initialize(IRestClient restClient);
}

public class RestService<TRestApiCall>(IRestClient restClient)
    where TRestApiCall : IRestApiCall
{
    protected readonly IRestClient _restClient = restClient;

    public virtual TApiCall CreateApiCall<TApiCall>() where TApiCall : TRestApiCall, new()
    {
        var apiCall = new TApiCall();
        apiCall.Initialize(_restClient);

        return apiCall;
    }

}
