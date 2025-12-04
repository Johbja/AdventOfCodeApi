using AdventOfCode.Application.Operations.Days.SubOperations;

namespace AdventOfCode.Application.Operations.Days;

public class Year2025Day01Operation 
    : ApplicationOperation<
        GenericOperationInput,
        GenericOperationOutput>
{
    protected override async Task<GenericOperationOutput> ExecuteApplicationLogic(GenericOperationInput input)
    {
        var lines = input.TextInput.Split(["\r\n", "\n", "\r"], StringSplitOptions.RemoveEmptyEntries);

        var operationPartOne = CreateSubOperation<Year2025Day01PartOneSubOperation>()
            .Execute(new Year2025Day01PartOneSubOperation.Input(lines));
        var resultPartOne = await operationPartOne.Output;

        var operationPartTwo = CreateSubOperation<Year2025Day01PartTwoSubOperation>()
            .Execute(new Year2025Day01PartTwoSubOperation.Input(lines));
        var resultPartTwo = await operationPartTwo.Output;

        return new GenericOperationOutput(
            $"Result for day 1 part one:{Environment.NewLine} password: {resultPartOne.Password}{Environment.NewLine} operation time: {operationPartOne.TimeElapsed}",
            $"Result for day 1 part two:{Environment.NewLine} password: {resultPartTwo.Password}{Environment.NewLine} operation time: {operationPartTwo.TimeElapsed}");
    }
}

