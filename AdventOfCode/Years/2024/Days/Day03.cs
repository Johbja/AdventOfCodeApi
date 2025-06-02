using AdventOfCode.Infrastructure.Attributes;
using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;
using System.Text.RegularExpressions;

namespace AdventOfCode.Years._2024.Days;

[DayInfo("Mull It Over", "Day 3")]
public class Day03(LoadInputService inputService) : ISolution
{
    private readonly string _input = inputService.GetInput(nameof(Day03), 2024);

    public void SolvePartOne()
    {
        int sum = FindInstruction(_input);
        Console.WriteLine(sum);
    }

    public void SolvePartTwo()
    {
        string devisionPattern = @"(.*?)don\'t(.*?)(?=do\(\)|$)";

        string cleanedInput = _input
            .Replace("\n\r", "")
            .Replace("\n", "")
            .Replace(Environment.NewLine, "") 
            + "don't()asa";

        int sum = Regex.Matches(cleanedInput, devisionPattern, RegexOptions.Multiline)
                       .Select(match => FindInstruction(match.Groups[1].Value))
                       .Sum();

        Console.WriteLine(sum);
    }

    private int FindInstruction(string input, string pattern = @"mul\((?:\d{1,3}\,\d{1,3})\)")
        => Regex.Matches(input, pattern)
                .Select(match =>
                {
                    return match.Value.Replace("mul", "")
                                      .Replace("(", "")
                                      .Replace(")", "")
                                      .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                      .Select(int.Parse)
                                      .Aggregate(1, (a, b) => a * b);
                })
                .Sum();
}
