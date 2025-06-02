using AdventOfCode.Infrastructure.Attributes;
using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;

namespace AdventOfCode.Years._2024.Days;

[DayInfo("Resonant Collinearity", "Day 8")]
public class Day08 : ISolution
{
    private readonly string[] _input;
    private readonly Dictionary<char, (int x, int y)[]> _antennaPositions;

    public Day08(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day08), 2024);
        _antennaPositions = _input.SelectMany((row, y) => row.Select((value, x) => (x, y, value)))
            .Where(x => x.value != '.')
            .GroupBy(x => x.value)
            .ToDictionary(group => group.Key, group => group.Select(g => (g.x, g.y)).ToArray());
    }

    public void SolvePartOne()
    {
        List<(int x, int y)> antiNodes = [];
        foreach(var antenna in _antennaPositions)
        {
            for(int i = 0; i < antenna.Value.Length; i++)
            {
                for(int j = 0; j < antenna.Value.Length; j++)
                {
                    if (i == j)
                        continue;

                    var direction = (x:antenna.Value[i].x - antenna.Value[j].x, y:antenna.Value[i].y - antenna.Value[j].y);
                    var antiNodePos = (x:antenna.Value[i].x + direction.x, y:antenna.Value[i].y + direction.y);

                    if (antiNodePos.x > 0 && antiNodePos.x < _input[0].Length && antiNodePos.y > 0 && antiNodePos.y < _input.Length)
                        antiNodes.Add(antiNodePos);
                }
            }
        }

        Console.WriteLine(antiNodes.Distinct().Count());

    }

    public void SolvePartTwo()
    {

    }



}
