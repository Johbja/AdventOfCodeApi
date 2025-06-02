using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Application.Operations.SubOperations;
public class Year2024Day01PartTwoSubOperation 
    : ApplicationOperation<Year2024Day01PartTwoSubOperation.Input,
                           Year2024Day01PartTwoSubOperation.Output>
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
        var idLists = input.Lines
            .Select(line => line
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(id => int.Parse(id))
                .ToArray())
            .Aggregate(
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

        var scoreMap = idLists.l.ToDictionary(id => id, number => 0);

        foreach (var id in idLists.r)
        {
            if (scoreMap.TryGetValue(id, out int value))
                scoreMap[id] = ++value;
        }

        var sum = scoreMap.Sum(scoreKey => scoreKey.Key * scoreKey.Value);

        return new Output(sum);
    }
}
