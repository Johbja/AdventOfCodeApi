using AdventOfCode.Application.Operations.Days.SubOperations;

namespace AdventOfCode.Application.Operations.Days;

public class Year2025Day03Operation
    : ApplicationOperation<
        GenericOperationInput,
        GenericOperationOutput>
{
    protected override async Task<GenericOperationOutput> ExecuteApplicationLogic(GenericOperationInput input)
    {
        var batteries = input.TextInput.Split(["\r\n", "\n", "\r"], StringSplitOptions.RemoveEmptyEntries)
            .Select(row
                => row.Select(value
                        => int.Parse($"{value}"))
                    .ToArray())
            .ToArray();

        var operationPartOne = CreateSubOperation<Year2025Day03PartOneSubOperation>()
            .Execute(new Year2025Day03PartOneSubOperation.Input(batteries));
        var resultPartOne = await operationPartOne.Output;

        var operationPartTwo = CreateSubOperation<Year2025Day03PartTwoSubOperation>()
            .Execute(new Year2025Day03PartTwoSubOperation.Input(batteries));
        var resultPartTwo = await operationPartTwo.Output;

        return new GenericOperationOutput(
            $"Result for day 3 part one:{Environment.NewLine} Jolts: {resultPartOne.JoltSum} {Environment.NewLine} operation time: {operationPartOne.TimeElapsed}",
            $"Result for day 3 part two:{Environment.NewLine} Jolts: {resultPartTwo.JoltSum} {Environment.NewLine} operation time: {operationPartTwo.TimeElapsed}");
    }
}
