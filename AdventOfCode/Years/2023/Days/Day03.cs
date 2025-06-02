using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;
using AdventOfCode.Infrastructure.Models._2023.Day03;
using AdventOfCode.Infrastructure.Attributes;

namespace AdventOfCode.Years._2023;

[DayInfo("Gear Ratios", "Day 3")]
public class Day03 : ISolution
{
    private readonly string[] _input;
    private readonly char[][] _engineMap;
    private readonly List<SerialNumber> _serialNumbers;

    public Day03(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day03), 2023);
        _engineMap = _input.Select(line => line.ToArray()).ToArray();

        _serialNumbers = _engineMap.SelectMany
        (
                (row, y) => row.Select((c, x) => new { Char = c, x, y })
                    .Where(x => char.IsDigit(x.Char))
                    .GroupBy(value => value.y)
                    .SelectMany
                    (
                        g => g.OrderBy(value => value.x)
                            .Select((v, i) => new { value = v, index = i })
                            .GroupBy(x => x.value.x - x.index)
                            .Select
                            (
                                x => new SerialNumber
                                {
                                    Positions = x.Select(x => (x.value.x, x.value.y, int.Parse(x.value.Char.ToString())))
                                        .ToList()
                                }
                            )
                    )
        )
        .ToList();
    }

    public void SolvePartOne()
    {
        var symbolsMap = _engineMap.SelectMany((row, y) => row.Select((value, x) => (x, y, value)))
            .Where(x => !char.IsDigit(x.value) && x.value != '.')
            .Select(value => (value.x, value.y))
            .ToDictionary(key => key, values => new List<SerialNumber>());

        foreach (var serialNumber in _serialNumbers)
        {
            var overlappingPositions = serialNumber.BoundingArea.Where(x => symbolsMap.ContainsKey(x));

            if (overlappingPositions.Any())
            {
                symbolsMap[overlappingPositions.First()].Add(serialNumber);
            }
        }

        var result = symbolsMap.Sum(x => x.Value.Sum(v => v.Value));

        Console.WriteLine(result);
    }

    public void SolvePartTwo()
    {
        var gearMap = _engineMap
            .SelectMany((row, y) => row.Select((value, x) => (x, y, value)))
            .Where(x => x.value == '*')
            .Select(x => (x.x, x.y))
            .ToDictionary(key => key, values => new List<SerialNumber>());

        foreach (var serialNumber in _serialNumbers)
        {
            var overlappingPositions = serialNumber.BoundingArea.Where(x => gearMap.ContainsKey(x));

            foreach (var position in overlappingPositions)
            {
                if (gearMap[position].Contains(serialNumber))
                    continue;

                gearMap[position].Add(serialNumber);
            }
        }

        int result = gearMap
            .Where(x => x.Value.Count == 2)
            .Sum(x => x.Value[0].Value * x.Value[1].Value);

        Console.WriteLine(result);
    }
}