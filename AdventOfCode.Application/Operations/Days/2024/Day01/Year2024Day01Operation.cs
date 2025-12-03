using AdventOfCode.Application.Operations.Days.SubOperations;

namespace AdventOfCode.Application.Operations.Days;

public class Year2024Day01Operation
    : ApplicationOperation<
        GenericOperationInput,
        GenericOperationOutput>
{
    protected override async Task<GenericOperationOutput> ExecuteApplicationLogic(GenericOperationInput input)
    {
        var lines = input.TextInput.Split(["\r\n", "\n", "\r"], StringSplitOptions.None);
        
        var operationDayOne = CreateSubOperation<Year2024Day01PartOneSubOperation>()
            .Execute(new Year2024Day01PartOneSubOperation.Input(lines));
        var resultDayOne = await operationDayOne.Output;

        var operationDayTwo = CreateSubOperation<Year2024Day01PartTwoSubOperation>()
            .Execute(new Year2024Day01PartTwoSubOperation.Input(lines));
        var resultDayTwo = await operationDayTwo.Output;

        
        return new GenericOperationOutput(
            $"Result for day1 part one:{Environment.NewLine} sum: {resultDayOne.Sum}{Environment.NewLine} operation time: {operationDayOne.TimeElapsed}",
            $"Result for day1 part two:{Environment.NewLine} sum: {resultDayTwo.Sum}{Environment.NewLine} operation time: {operationDayTwo.TimeElapsed}");
    }
}
