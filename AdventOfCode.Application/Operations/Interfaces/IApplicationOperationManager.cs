namespace AdventOfCode.Application.Operations.Interfaces;

public interface IApplicationOperationManager
{
    ITraceableOutput<Task<TOutput>> HandleExecute<TOutput, TInput>(
        Func<TInput, ITraceableOutput<Task<TOutput>>> operationLogic, 
        TInput input) 
        where TInput : class
        where TOutput : class;
}
