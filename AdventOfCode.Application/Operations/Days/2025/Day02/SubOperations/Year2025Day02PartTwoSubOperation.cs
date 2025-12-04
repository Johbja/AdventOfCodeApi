using AdventOfCode.Application.Attributes;

namespace AdventOfCode.Application.Operations.Days.SubOperations;

[TestableOperation]
public class Year2025Day02PartTwoSubOperation 
    : ApplicationOperation<
        Year2025Day02PartTwoSubOperation.Input,
        Year2025Day02PartTwoSubOperation.Output>
{
    public class Input(List<long[]> ranges)
    {
        public IReadOnlyList<long[]> Ranges { get; } = ranges;
    }

    public class Output(List<long> invalidIds, long invalidIdSum)
    {
        public List<long> invalidIds { get; } = invalidIds;
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
                    var maxChunkSize = currentValue.Length / 2;
                    for (var chunkSize = maxChunkSize; chunkSize >= 1; chunkSize--)
                    {
                        var subStrings = currentValue.Chunk(chunkSize)
                            .Select(chunk => new string(chunk))
                            .ToArray();

                        if (!ChunksIsEqual(subStrings))
                            continue;

                        invalidIds.Add(i);
                        break;
                    }
                }
            }

            var invalidIdSum = invalidIds.Sum();

            return new Output(invalidIds, invalidIdSum);

            static bool ChunksIsEqual(string[] subStrings)
            {
                for (var chunk = 1; chunk < subStrings.Length; chunk++)
                {
                    if (subStrings[chunk - 1] != subStrings[chunk])
                        return false;
                }

                return true;
            }
        });
    }
}

