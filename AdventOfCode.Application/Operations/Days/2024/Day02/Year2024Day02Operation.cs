namespace AdventOfCode.Application.Operations.Days;

public class Year2024Day02Operation
    : ApplicationOperation<
        GenericOperationInput, 
        GenericOperationOutput>
{

    protected override async Task<GenericOperationOutput> ExecuteApplicationLogic(GenericOperationInput input)
    {
        return new GenericOperationOutput($"Day {2}_{input.TextInput}_1", $"Day {2}_{input.TextInput}_2");
    }

}
