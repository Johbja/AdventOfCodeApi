namespace AdventOfCode.Application.RestServices.ApiCalls;

public abstract class RestApiCallWithInput<TInput> : RestApiCallBase<TInput, Null>
    where TInput : class
{

}
