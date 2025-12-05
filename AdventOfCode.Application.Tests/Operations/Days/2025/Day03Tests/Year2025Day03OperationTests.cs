using AdventOfCode.Application.Operations.Year2025.Days.SubOperations;
using AdventOfCode.Application.Tests.Fixtures;
using AdventOfCode.Application.Tests.Operations;

namespace AdventOfCode.Application.Tests.Year2025.Operations;

public class Year2025Day03OperationTests(AdventOfCodeOperationsFixture fixture)
    : OperationsTestHarness(fixture)
{
    [Fact]
    public async Task SolveDay03PartOneWithSampleInput()
    {
        var op = CreateOperation<Year2025Day03PartOneSubOperation>()
            .Execute(new Year2025Day03PartOneSubOperation.Input(CreateSampleInput()));
        var result = await op.Output;

        Assert.Equal(357, result.JoltSum);
    }

    [Fact]
    public async Task SolveDay03PartTwoWithSampleInput()
    {
        var op = CreateOperation<Year2025Day03PartTwoSubOperation>()
            .Execute(new Year2025Day03PartTwoSubOperation.Input(CreateSampleInput()));
        var result = await op.Output;

        Assert.Equal(3121910778619, result.JoltSum);
    }

    private int[][] CreateSampleInput()
    {
        string input = """
                       987654321111111
                       811111111111119
                       234234234234278
                       818181911112111
                       """;

        return input.Split(["\r\n", "\n", "\r"], StringSplitOptions.RemoveEmptyEntries)
            .Select(row 
                => row.Select(value 
                    => int.Parse($"{value}"))
                    .ToArray())
            .ToArray();
    }
}

