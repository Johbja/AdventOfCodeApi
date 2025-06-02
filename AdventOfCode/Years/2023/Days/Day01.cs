using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;
using AdventOfCode.Infrastructure.Attributes;
using System.Text.RegularExpressions;

namespace AdventOfCode.Years._2023;

[DayInfo("Trebuchet?!", "Day 1")]
public class Day01(LoadInputService inputService) : ISolution
{
    private readonly string[] _input = inputService.GetInputAsLines(nameof(Day01), 2023);

    public void SolvePartOne()
    {
        int sum = 0;
        foreach (var row in _input)
        {
            var matches = Regex.Matches(row, @"\d");
            sum += int.Parse($"{matches[0].Value}{matches[^1].Value}");
        }

        Console.WriteLine(sum);
    }

    public void SolvePartTwo()
    {
        int sum = 0;

        for (int i = 0; i < _input.Length; i++)
        {
            string row = _input[i];

            row = row.Replace("one", "o1e")
                .Replace("two", "t2o")
                .Replace("three", "t3e")
                .Replace("four", "f4r")
                .Replace("five", "f5e")
                .Replace("six", "s6x")
                .Replace("seven", "s7n")
                .Replace("eight", "e8t")
                .Replace("nine", "n9e");

            var matches = Regex.Matches(row, @"\d");
            var fistMatch = matches[0];
            var lastMatch = matches[^1];

            sum += int.Parse($"{fistMatch}{lastMatch}");
        }

        Console.WriteLine(sum);
    }

}
