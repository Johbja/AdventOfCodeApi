using AdventOfCode.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Application.Operations.SubOperations;
public class Year2024Day01PartOneSubOperation :
    ApplicationOperation<
        Year2024Day01PartOneSubOperation.Input,
        Year2024Day01PartOneSubOperation.Output>
{
    public class Input(string[] lines)
    {
        public string[] Lines { get; } = lines;
    }

    public class Output(int sum)
    {
        public int Sum { get; } = sum;
    }

    protected override async Task<Output> ExecuteApplicationLogic(Input input)
    {
        var splitLiens = input.Lines
            .Select(line => line
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Where(x => x.HasValue())
                .Select(id => int.Parse(id))
                .ToArray())
            .Where(values => values.Length >= 2);

        var idLists = splitLiens.Aggregate(
            new
            {
                l = new List<int>(),
                r = new List<int>()
            },
            (a, b) =>
            {
                a.l.Add(b[0]);
                a.r.Add(b[1]);
                return a;
            });

        var leftList = idLists.l.OrderBy(x => x).ToArray();
        var rightList = idLists.r.OrderBy(x => x).ToArray();

        int sum = 0;
        for (int i = 0; i < leftList.Length; i++)
        {
            sum += Math.Abs(leftList[i] - rightList[i]);
        }

        return new Output(sum);
    }
}
