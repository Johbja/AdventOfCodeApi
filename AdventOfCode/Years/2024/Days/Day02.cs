using AdventOfCode.Infrastructure.Attributes;
using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Years._2024.Days;

[DayInfo("Red-Nosed Reports", "Day 2")]
public class Day02(LoadInputService inputService) : ISolution
{
    private readonly string[] _input = inputService.GetInputAsLines(nameof(Day02), 2024);

    public void SolvePartOne()
    {
        var reports = _input
            .Select(x 
                => x.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.Parse(x))
                    .ToArray())
            .ToArray();

        int safeCounter = 0;
        foreach (var report in reports) 
        {
            if(CheckReport(report))
               safeCounter++;
        }

        Console.WriteLine(safeCounter);
    }

    public void SolvePartTwo()
    {
        var reports = _input
            .Select(x 
                => x.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.Parse(x))
                    .ToArray())
            .ToArray();

        int safeCounter = 0;
        foreach (var report in reports)
        {
            if (CheckReport(report) || DiagonseReport(report))
            {
                safeCounter++;
            }
        }

        Console.WriteLine(safeCounter);
    }

    private bool DiagonseReport(int[] report)
    {
        for(int i = 0; i < report.Length; i++)
        {
            var modifiedReport = report
                .Where((_, index) => index != i)
                .ToArray();

            if (CheckReport(modifiedReport))
                return true;
        }

        return false;
    }

    private bool CheckReport(int[] report)
    {
        return report.Zip(report.Skip(1), (a, b) => a < b && Math.Abs(a - b) <= 3).All(x => x)
            || report.Zip(report.Skip(1), (a, b) => a > b && Math.Abs(a - b) <= 3).All(x => x);
    }
    
}
