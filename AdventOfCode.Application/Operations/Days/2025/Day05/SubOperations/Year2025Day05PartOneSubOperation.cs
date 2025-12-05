using AdventOfCode.Application.Attributes;

namespace AdventOfCode.Application.Operations.Year2025.Days.SubOperations;

[TestableOperation]
public class Year2025Day05PartOneSubOperation
    : ApplicationOperation<
        Year2025Day05PartOneSubOperation.Input, 
        Year2025Day05PartOneSubOperation.Output>
{
    public class Input(long[][] ranges, long[] ids)
    {
        public long[][] Ranges { get; } = ranges;
        public long[] ids { get; } = ids;
    }

    public class Output(long[] validIds)
    {
        public long[] ValidIds { get; } = validIds;
        public long validIdCount => ValidIds.Length;
    }

    protected override async Task<Output> ExecuteApplicationLogic(Input input)
    {
        return await Task.Run(() =>
        {
            var validIds = input.ids
                .Where(id 
                    => input.Ranges.Any(range 
                        => id >= range[0] && id <= range[1]))
                .ToArray();

            return new Output(validIds);
        });
    }
}

