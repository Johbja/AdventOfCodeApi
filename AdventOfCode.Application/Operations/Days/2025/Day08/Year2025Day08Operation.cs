
using AdventOfCode.Application.Operations.Year2025.Days.SubOperations;

namespace AdventOfCode.Application.Operations.Year2025.Days;

public class Year2025Day08Operation
    : ApplicationOperation<
        GenericOperationInput,
        GenericOperationOutput>
{
    protected override async Task<GenericOperationOutput> ExecuteApplicationLogic(GenericOperationInput input)
    {
        var points = input.TextInput.Split(["\r\n", "\n", "\r"], StringSplitOptions.RemoveEmptyEntries).Select(
            (row, i) =>
            {
                var cords = row.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                return new Year2025Day08PartOneSubOperation.Vector3d(cords, i);
            }).ToArray();

        var operationPartOne = CreateSubOperation<Year2025Day08PartOneSubOperation>()
            .Execute(new Year2025Day08PartOneSubOperation.Input(points, 1000));
        var resultPartOne = await operationPartOne.Output;

        var operationPartTwo = CreateSubOperation<Year2025Day08PartTwoSubOperation>()
            .Execute(new Year2025Day08PartTwoSubOperation.Input());
        var resultPartTwo = await operationPartTwo.Output;

        return new GenericOperationOutput(
            $"Result for day 4 part one:{Environment.NewLine} largest circuits: {resultPartOne.CordLength} {Environment.NewLine} operation time: {operationPartOne.TimeElapsed}",
            $"Result for day 4 part two:{Environment.NewLine} p2: {resultPartTwo} {Environment.NewLine} operation time: {operationPartTwo.TimeElapsed}");

    }
}

