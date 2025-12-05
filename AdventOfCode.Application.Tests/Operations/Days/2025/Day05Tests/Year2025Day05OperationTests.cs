using AdventOfCode.Application.Operations.Year2025.Days.SubOperations;
using AdventOfCode.Application.Tests.Fixtures;
using AdventOfCode.Application.Tests.Operations;

namespace AdventOfCode.Application.Tests.Year2025.Operations;

public class Year2025Day05OperationTests(AdventOfCodeOperationsFixture fixture) 
    : OperationsTestHarness(fixture)
{
    [Fact]
    public async Task SolveDay05PartOneWithSampleInput()
    {
        var input = CreateSampleInput();
        var op = CreateOperation<Year2025Day05PartOneSubOperation>()
            .Execute(new Year2025Day05PartOneSubOperation.Input(input.ranges, input.ids));
        var result = await op.Output;

        Assert.Equal(3, result.validIdCount);
    }

    [Fact]
    public async Task SolveDay05PartTwoWithSampleInput()
    {
        var input = CreateSampleInput();
        var op = CreateOperation<Year2025Day05PartTwoSubOperation>()
            .Execute(new Year2025Day05PartTwoSubOperation.Input(input.ranges));
        var result = await op.Output;

        Assert.Equal(14, result.DistinctIds);
    }

    private (long[][] ranges, long[] ids) CreateSampleInput()
    {
        const string input = """
                       3-5
                       10-14
                       16-20
                       12-18

                       1
                       5
                       8
                       11
                       17
                       32
                       """;

        var result = input.Split(["\r\n", "\n", "\r"], StringSplitOptions.None).ToList();
        var splitIndex = result.IndexOf("");
        var ranges = result.Take(splitIndex)
            .Select(range 
                => range.Split("-").Select(long.Parse).ToArray())
            .ToArray();
        var ids = result.Skip(splitIndex + 1).Select(long.Parse).ToArray();
        return (ranges, ids);
    }
}

