namespace AdventOfCode.Application.Operations.Interfaces;

public interface IApplicationOperation
{
    void Initialize(
        IApplicationOperationManager operationManager,
        IApplicationOperationResolver operationResolver);
}
