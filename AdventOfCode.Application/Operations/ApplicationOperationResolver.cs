using AdventOfCode.Application.Operations.Interfaces;

namespace AdventOfCode.Application.Operations;

public class ApplicationOperationResolver(
    IApplicationOperationManager operationManager,
    IServiceProvider serviceProvider)
    : IApplicationOperationResolver
{
    private readonly IApplicationOperationManager _operationManager = operationManager;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    TOperation IApplicationOperationResolver.ResolveOperation<TOperation>()
    {
        var operation = CreateOperation(typeof(TOperation));
        return (TOperation)operation;
    }

    private IApplicationOperation CreateOperation(Type operation)
    {
        //TODO: add transaction scope in future if needed
        var operationInstance = _serviceProvider.GetService(operation) as IApplicationOperation;

        if(operationInstance == null)
        {
            throw new NullReferenceException($"Operation of type {operation.FullName} was not found in {_serviceProvider.GetType().FullName}");
        }

        InitializeOperation(operationInstance);
        return operationInstance;
    }

    private void InitializeOperation(IApplicationOperation operation)
    {
        operation.Initialize(
            _operationManager,
            this);
    }
}
