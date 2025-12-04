using AdventOfCode.Application.Attributes;
using System.Reflection;

namespace AdventOfCode.Application.Operations.Days.SubOperations;

[TestableOperation]
public class Year2025Day03PartTwoSubOperation
    : ApplicationOperation<
        Year2025Day03PartTwoSubOperation.Input,
        Year2025Day03PartTwoSubOperation.Output>
{
    public class Input(int[][] batteries)
    {
        public int[][] Batteries { get; } = batteries;
    }

    public class Output(List<long> gridJolts, long joltSum)
    {
        public List<long> invalidIds { get; } = gridJolts;
        public long JoltSum { get; } = joltSum;
    }

    protected override async Task<Output> ExecuteApplicationLogic(Input input)
    {
        var finalNumberLength = 12;

        return await Task.Run(() =>
        {
            List<long> gridJolts = [];
            foreach (var batteryArray in input.Batteries)
            {
                List<string> numbers = [];
                var currentSearchStart = 0;
                var arrayLength = batteryArray.Length;

                for (var i = 0; i < finalNumberLength; i++)
                {
                    var maxSeachIndex = arrayLength - (finalNumberLength - i);
                    var maxNumber = -1;
                    var maxIndex = -1;

                    for (var j = currentSearchStart; j <= maxSeachIndex; j++)
                    {
                        if (batteryArray[j] <= maxNumber)
                            continue;

                        maxNumber = batteryArray[j];
                        maxIndex = j;
                    }

                    numbers.Add(maxNumber.ToString());
                    currentSearchStart = maxIndex + 1;
                }

                gridJolts.Add(long.Parse(string.Join("", numbers)));
            }

            return new Output(gridJolts, gridJolts.Sum());
        });
    }
}

