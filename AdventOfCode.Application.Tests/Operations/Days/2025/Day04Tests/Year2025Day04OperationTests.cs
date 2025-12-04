using AdventOfCode.Application.Operations.Days.SubOperations;
using AdventOfCode.Application.Tests.Fixtures;

namespace AdventOfCode.Application.Tests.Operations;

public class Year2025Day04OperationTests(AdventOfCodeOperationsFixture fixture) 
    : OperationsTestHarness(fixture)
{
    [Fact]
    public async Task SolveForDay04PartOneWithSampleInput()
    {
        var op = CreateOperation<Year2025Day04PartOneSubOperation>()
            .Execute(new Year2025Day04PartOneSubOperation.Input(CrateSampleInput()));
        var result = await op.Output;
        
        Assert.Equal(13, result.Rolls);
    }

    [Fact]
    public async Task SolveForDay04PartTwoWithSampleInput()
    {
        var op = CreateOperation<Year2025Day04PartTwoSubOperation>()
            .Execute(new Year2025Day04PartTwoSubOperation.Input(CrateSampleInput()));
        var result = await op.Output;

        Assert.Equal(43, result.Rolls);
    }

    private int[][] CrateSampleInput()
    {
        var input = """
                       ..@@.@@@@.
                       @@@.@.@.@@
                       @@@@@.@.@@
                       @.@@@@..@.
                       @@.@@@@.@@
                       .@@@@@@@.@
                       .@.@.@.@@@
                       @.@@@.@@@@
                       .@@@@@@@@.
                       @.@.@@@.@.
                       """;

       return input.Split(["\r\n", "\n", "\r"], StringSplitOptions.RemoveEmptyEntries)
           .Select(row
               => row.Select(cell => cell == '@' ? 1 : 0)
                   .ToArray())
           .ToArray();
    }
}

