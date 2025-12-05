using AdventOfCode.Application.Attributes;

namespace AdventOfCode.Application.Operations.Year2025.Days.SubOperations;

[TestableOperation]
public class Year2025Day02PartOneSubOperation 
    : ApplicationOperation<
        Year2025Day02PartOneSubOperation.Input,
        Year2025Day02PartOneSubOperation.Output>
{
    public class Input(List<long[]> ranges)
    {
        public IReadOnlyList<long[]> Ranges { get; } = ranges;
    }

    public class Output(long invalidIdSum)
    {
        public long InvalidIdSum { get; } = invalidIdSum;
    }

    protected override async Task<Output> ExecuteApplicationLogic(Input input)
    {
        return await Task.Run(() =>
        {
            List<long> invalidIds = [];
            foreach (var range in input.Ranges)
            {
                for (var i = range[0]; i <= range[1]; i++)
                {
                    var currentValue = i.ToString();
                    if(currentValue.Length % 2 != 0)
                        continue;

                    var splitIndex = (currentValue.Length / 2);
                    var first = currentValue.Substring(0, splitIndex);
                    var second = currentValue.Substring(splitIndex);

                    if(first == second)
                        invalidIds.Add(i);
                }
            }

            var invalidIdSum= invalidIds.Sum();

            return new Output(invalidIdSum);
        });
    }
}

