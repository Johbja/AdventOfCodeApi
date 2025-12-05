using AdventOfCode.Application.Operations.Year2025.Days.SubOperations;

namespace AdventOfCode.Application.Operations.Year2025.Days;

public class Year2025Day05Operation
    : ApplicationOperation<
        GenericOperationInput,
        GenericOperationOutput>
{
    protected override async Task<GenericOperationOutput> ExecuteApplicationLogic(GenericOperationInput input)
    {
        var result = input.TextInput.Split(["\r\n", "\n", "\r"], StringSplitOptions.None).ToList();
        var splitIndex = result.IndexOf("");
        var ranges = result.Take(splitIndex)
            .Select(range
                => range.Split("-").Select(long.Parse).ToArray())
            .ToArray();
        var ids = result.Skip(splitIndex + 1)
            .Select(value =>
            {
                var parsed = long.TryParse(value, out var parsedValue);
                return parsed ? parsedValue : -1;
            })
            .Where(x => x > 0)
            .ToArray();

        var operationPartOne = CreateSubOperation<Year2025Day05PartOneSubOperation>()
            .Execute(new Year2025Day05PartOneSubOperation.Input(ranges, ids));
        var resultPartOne = await operationPartOne.Output;

        var operationPartTwo = CreateSubOperation<Year2025Day05PartTwoSubOperation>()
            .Execute(new Year2025Day05PartTwoSubOperation.Input(ranges));
        var resultPartTwo = await operationPartTwo.Output;

        return new GenericOperationOutput(
            $"Result for day 4 part one:{Environment.NewLine} Spolied fruts: {resultPartOne.validIdCount} {Environment.NewLine} operation time: {operationPartOne.TimeElapsed}",
            $"Result for day 4 part two:{Environment.NewLine} Distinct fresh ingredient Ids: {resultPartTwo.DistinctIds} {Environment.NewLine} operation time: {operationPartTwo.TimeElapsed}");
    }
}

