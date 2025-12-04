using AdventOfCode.Application.Operations.Days.SubOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Application.Operations.Days;

public class Year2025Day02Operation 
    : ApplicationOperation<
        GenericOperationInput, 
        GenericOperationOutput>
{
    protected override async Task<GenericOperationOutput> ExecuteApplicationLogic(GenericOperationInput input)
    {
        var idRanges = input.TextInput.Split(",", StringSplitOptions.RemoveEmptyEntries);
        var ranges = idRanges.Select(range =>
        {
            var ranges = range.Split("-", StringSplitOptions.RemoveEmptyEntries);
            return new long[] { long.Parse(ranges[0]), long.Parse(ranges[1]) };
        }).ToList();

        var operationPartOne = CreateSubOperation<Year2025Day02PartOneSubOperation>()
            .Execute(new Year2025Day02PartOneSubOperation.Input(ranges));
        var resultPartOne = await operationPartOne.Output;

        var operationPartTwo = CreateSubOperation<Year2025Day02PartTwoSubOperation>()
            .Execute(new Year2025Day02PartTwoSubOperation.Input(ranges));
        var resultPartTwo = await operationPartTwo.Output;

        return new GenericOperationOutput(
            $"Result for day2 part one:{Environment.NewLine} Invalid sum: {resultPartOne.InvalidIdSum} {Environment.NewLine} operation time: {operationPartOne.TimeElapsed}",
            $"Result for day2 part two:{Environment.NewLine} Invalid sum: {resultPartTwo.InvalidIdSum} {Environment.NewLine} operation time: {operationPartTwo.TimeElapsed}");
    }
}

