
using AdventOfCode.Application.Attributes;

namespace AdventOfCode.Application.Operations.Year2025.Days.SubOperations;

[TestableOperation]
public class Year2025Day05PartTwoSubOperation
        : ApplicationOperation<
            Year2025Day05PartTwoSubOperation.Input,
            Year2025Day05PartTwoSubOperation.Output>
{
    public class Input(long[][] ranges)
    {
        public class Range(long start, long end)
        {
            public long Start { get; set; } = start;
            public long End { get; set; } = end;
        }

        public Range[] Ranges { get; } = ranges
            .Select(r => new Range(r[0], r[1]))
            .ToArray();
    }

    public class Output(long distinctIds)
    {
        public long DistinctIds { get; } = distinctIds;
    }

    protected override Task<Output> ExecuteApplicationLogic(Input input)
    {
        return Task.Run(() =>
        {
            var rangesOrdered = input.Ranges.OrderBy(x => x.Start);
            List<Input.Range> distinctRanges = [];

            foreach (var range in rangesOrdered)
            {
                if (!distinctRanges.Any())
                {
                    distinctRanges.Add(range);
                    continue;
                }

                var lastRange = distinctRanges.Last();
                if (range.Start > lastRange.End)
                {
                    distinctRanges.Add(range);
                    continue;
                }

                var newEnd = Math.Max(lastRange.End, range.End);
                distinctRanges[^1].End = newEnd;
            }

            var disitnctIds= distinctRanges.Sum(r => r.End - r.Start + 1);

            return new Output(disitnctIds);
        });
    }
}

