using AdventOfCode.Application;
using AdventOfCode.Application.Operations.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdventOfCode.Api;
[ApiController]
public class ApiController(IApplicationOperationResolver operationResolver) : ControllerBase
{
    private List<object> _baseResponses = [];
    
    protected TOperation CreateOperation<TOperation>() 
        where TOperation : class, 
        IApplicationOperation
    {
        return operationResolver.ResolveOperation<TOperation>();
    }

    protected async Task<BaseResponse<TOutput>> CreateBaseResponse<TOutput>(
        ITraceableOutput<Task<TOutput>> traceableOutput)
    {
        var result = await traceableOutput.Output;
        traceableOutput.Stopwatch.Stop();
        var response = new BaseResponse<TOutput>(traceableOutput.TimeElapsed, result);

        _baseResponses.Add(response);

        return response;
    }

    protected IReadOnlyCollection<object> GetCurrentResponses => _baseResponses;

    public class BaseResponse<T>(string timeElapsed, T result)
    {
        public T Result { get; } = result;
        public string TimeElapsed { get; } = timeElapsed;
    }
}
