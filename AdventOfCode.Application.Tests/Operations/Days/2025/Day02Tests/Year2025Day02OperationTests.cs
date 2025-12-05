using AdventOfCode.Application.Operations.Year2025.Days.SubOperations;
using AdventOfCode.Application.Tests.Fixtures;
using AdventOfCode.Application.Tests.Operations;

namespace AdventOfCode.Application.Tests.Year2025.Operations;

public class Year2025Day02OperationTests(AdventOfCodeOperationsFixture fixture)
    : OperationsTestHarness(fixture)
{
    [Fact]
    public async Task SolveDay02PartOneWithSampleInput()
    {
        var op = CreateOperation<Year2025Day02PartOneSubOperation>()
            .Execute(new Year2025Day02PartOneSubOperation.Input(
                CreateSampleInput()));
        var result = await op.Output;

        Assert.Equal(1227775554, result.InvalidIdSum);
    }

    [Fact]
    public async Task SolveDay02PartTwoWithSampleInput()
    {
        var op = CreateOperation<Year2025Day02PartTwoSubOperation>()
            .Execute(new Year2025Day02PartTwoSubOperation.Input(
                CreateSampleInput()));
        var result = await op.Output;

        Assert.Equal(4174379265, result.InvalidIdSum);
    }

    private List<long[]> CreateSampleInput()
    {
        string inputText =
            "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";

        var idRanges = inputText.Split(",", StringSplitOptions.RemoveEmptyEntries);
        var ranges = idRanges.Select(range =>
        {
            var ranges = range.Split("-", StringSplitOptions.RemoveEmptyEntries);
            return new long[] { long.Parse(ranges[0]), long.Parse(ranges[1]) };
        }).ToList();

        return ranges;
    }
}

