using AdventOfCode.Application.Operations.Days.SubOperations;

namespace AdventOfCode.Application.Operations.Days;

public class Year2025Day04Operation 
    : ApplicationOperation<
        GenericOperationInput, 
        GenericOperationOutput>
{
    protected override async Task<GenericOperationOutput> ExecuteApplicationLogic(GenericOperationInput input)
    {
        var batteries = input.TextInput.Split(["\r\n", "\n", "\r"], StringSplitOptions.RemoveEmptyEntries)
            .Select(row
                => row.Select(cell => cell == '.' ? 0 : 1)
                    .ToArray())
            .ToArray();

        var operationPartOne = CreateSubOperation<Year2025Day04PartOneSubOperation>()
            .Execute(new Year2025Day04PartOneSubOperation.Input(batteries));
        var resultPartOne = await operationPartOne.Output;

        var operationPartTwo = CreateSubOperation<Year2025Day04PartTwoSubOperation>()
            .Execute(new Year2025Day04PartTwoSubOperation.Input(batteries));
        var resultPartTwo = await operationPartTwo.Output;

        return new GenericOperationOutput(
            $"Result for day 4 part one:{Environment.NewLine} Jolts: {resultPartOne.Rolls} {Environment.NewLine} operation time: {operationPartOne.TimeElapsed}",
            $"Result for day 4 part two:{Environment.NewLine} Jolts: {resultPartTwo.Rolls} {Environment.NewLine} operation time: {operationPartTwo.TimeElapsed}");
    }
}

