using AdventOfCode.Infrastructure.Attributes;
using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Years._2024.Days;

[DayInfo("Historian Hysteria", "Day 1")]
public class Day01(LoadInputService inputService) : ISolution
{
    private readonly string[] _input = inputService.GetInputAsLines(nameof(Day01), 2024);

    public void SolvePartOne()
    {
        var idLists = _input.Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(id => int.Parse(id)).ToArray())
            .Aggregate(new { l = new List<int>(), r = new List<int>() }, (a, b) => { a.l.Add(b[0]); a.r.Add(b[1]); return a; });

        var leftList = idLists.l.OrderBy(x => x).ToArray();
        var rightList = idLists.r.OrderBy(x => x).ToArray();

        int sum = 0;
        for (int i=0; i < leftList.Length; i++)
        {
            sum += Math.Abs(leftList[i] - rightList[i]);
        }

        Console.WriteLine(sum);
    }

    public void SolvePartTwo()
    {
        var idLists = _input.Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(id => int.Parse(id)).ToArray())
            .Aggregate(new { l = new List<int>(), r = new List<int>() }, (a, b) => { a.l.Add(b[0]); a.r.Add(b[1]); return a; });

        var scoremap = idLists.l.ToDictionary(id => id, number => 0);

        foreach (var id in idLists.r) 
        { 
            if(scoremap.TryGetValue(id, out int value))
                scoremap[id] = ++value;
        }

        var sum = scoremap.Sum(scoreKey => scoreKey.Key * scoreKey.Value);

        Console.WriteLine(sum);
    }
}
