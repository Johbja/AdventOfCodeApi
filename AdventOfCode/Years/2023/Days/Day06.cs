using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;
using AdventOfCode.Infrastructure.Attributes;

namespace AdventOfCode.Years._2023;

[DayInfo("Wait For It", "Day 6")]
public class Day06 : ISolution
{
    private readonly string[] _input;
    private readonly Dictionary<int, int> _timeToDistannceMap;

    public Day06(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day06), 2023);
        var time = _input[0].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        var distance = _input[1].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        _timeToDistannceMap = new();

        for(int i = 0; i < time.Count(); i++)
        {
            _timeToDistannceMap.Add(time[i], distance[i]);
        }
    }

    public void SolvePartOne()
    {
        List<int> recordCount = new();
        foreach(var key in _timeToDistannceMap.Keys)
        {
            int distanceToBeat = _timeToDistannceMap[key];
            int time = key;

            int counter = 0;

            for(int i = 1; i < key; i++)
            {
                int remaningTime = time - i;
                int distancsWithAcc = remaningTime * i;

                if (distancsWithAcc > distanceToBeat)
                    counter++;
            }
            recordCount.Add(counter);

        }

        int result = recordCount.Aggregate((a, b) => a * b);

        Console.WriteLine(result);
    }

    public void SolvePartTwo()
    {
        double actualTime = double.Parse(_timeToDistannceMap.Keys.Aggregate("", (a, b) => a + b.ToString()));
        double actualDistance = double.Parse(_timeToDistannceMap.Values.Aggregate("", (a, b) => a + b.ToString()));

        int counter = 0;
        for(int i = 1; i < actualTime; i++)
        {
            double remaningTime = actualTime - i;
            double distancsWithAcc = remaningTime * i;

            if (distancsWithAcc > actualDistance)
                counter++;
        }

        Console.WriteLine(counter);

    }
}
