using AdventOfCode.Application.Attributes;

namespace AdventOfCode.Application.Operations.Days.SubOperations;

[TestableOperation]
public class Year2025Day03PartOneSubOperation
    : ApplicationOperation<
        Year2025Day03PartOneSubOperation.Input,
        Year2025Day03PartOneSubOperation.Output>
{
    public class Input(int[][] batteries)
    {
        public int[][] Batteries { get; } = batteries;
    }

    public class Output(List<int> gridJolts, int joltSum)
    {
        public List<int> invalidIds { get; } = gridJolts;
        public int JoltSum { get; } = joltSum;
    }

    protected override async Task<Output> ExecuteApplicationLogic(Input input)
    {
        return await Task.Run(() =>
        {
            List<int> gridJolts = [];
            foreach (var batteryArray in input.Batteries)
            {
                var topJolt = 0;
                for (var batteryOne = 0; batteryOne < batteryArray.Length-1; batteryOne++)
                {
                    var firstBattery = batteryArray[batteryOne] * 10;
                    for (var batteryTwo = batteryOne + 1; batteryTwo < batteryArray.Length; batteryTwo++)
                    {
                        var jolts = firstBattery + batteryArray[batteryTwo];
                        if(jolts >= topJolt)
                            topJolt = jolts;
                    }
                }
                gridJolts.Add(topJolt);
            }

            return new Output(gridJolts, gridJolts.Sum());
        });


    }
}

