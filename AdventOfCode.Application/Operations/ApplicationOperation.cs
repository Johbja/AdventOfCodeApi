using AdventOfCode.Application.Operations.Interfaces;
using System.Diagnostics;

namespace AdventOfCode.Application.Operations;

public abstract class ApplicationOperationBase<TInput, TOutput> 
    : IApplicationOperation
    where TInput : class
    where TOutput : class
{
    private IApplicationOperationManager _operationManager;
    private IApplicationOperationResolver _operationResolver;
    private string OperationName => GetType().Name;

    protected abstract Task<TOutput> ExecuteApplicationLogicInternal(TInput input);

    public ITraceableOutput<Task<TOutput>> Execute(TInput input)
    {
        return _operationManager.HandleExecute(ExecuteInternal, input);
    }

    private ITraceableOutput<Task<TOutput>>ExecuteInternal(TInput input = null)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            var result = ExecuteApplicationLogicInternal(input);
            var output = new TraceableOutput<Task<TOutput>>(stopwatch, result);
            
            return output;
        }
        catch(Exception ex)
        {
            throw HandleException(ex, stopwatch);
        }
    }

    private Exception HandleException(Exception ex, Stopwatch stopwatch)
    {
        return new Exception($"Error occured in operation {OperationName}, time elapsed: {stopwatch.Elapsed.TotalMilliseconds}ms", ex);
    }

    public TOperation CreateSubOperation<TOperation>() 
        where TOperation : class, IApplicationOperation
    {
        var operation = _operationResolver.ResolveOperation<TOperation>();
        return operation;
    }

    public void Initialize(
        IApplicationOperationManager operationManager, 
        IApplicationOperationResolver operationResolver)
    {
        _operationManager = operationManager;
        _operationResolver = operationResolver;
    }
}

public abstract class ApplicationOperation<TInput, TOutput> 
    : ApplicationOperationBase<TInput, TOutput>
    where TInput : class 
    where TOutput : class 
{
    protected abstract Task<TOutput> ExecuteApplicationLogic(TInput input);
    protected override Task<TOutput> ExecuteApplicationLogicInternal(TInput input) 
        => ExecuteApplicationLogic(input);
}

public abstract class ApplicationOperationWithInput<TInput> 
    : ApplicationOperationBase<TInput, Null>
    where TInput : class
{
    public abstract Task ExecuteApplicationLogic(TInput input);
    protected override Task<Null> ExecuteApplicationLogicInternal(TInput input)
    {
        ExecuteApplicationLogic(input);
        return Task.Run(() => { return new Null(); });
    }
}

public abstract class ApplicationOperationWithOutput<TOutput> 
    : ApplicationOperationBase<Null, TOutput>
    where TOutput : class
{
    public abstract Task<TOutput> ExecuteApplicationLogic();
    protected override Task<TOutput> ExecuteApplicationLogicInternal(Null input)
      => ExecuteApplicationLogic(); 
}
