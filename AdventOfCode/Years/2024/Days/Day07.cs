using AdventOfCode.Infrastructure.Attributes;
using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;

namespace AdventOfCode.Years._2024.Days;

[DayInfo("Bridge Repair", "Day 7")]
public class Day07 : ISolution
{
    private readonly string[] _input;
    private readonly List<(long key, long[] numbers)> _equations;
    private Func<long, long, long>[] _operators =
    {
        (a, b) => a + b,
        (a, b) => a * b,
    };

    public Day07(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day07), 2024);

        _equations = _input.Select(equation =>
        {
            var values = equation.Split(":", StringSplitOptions.RemoveEmptyEntries);
            long key = long.Parse(values[0]);
            long[] numbers = values[1]
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(number => long.Parse(number))
                .ToArray();

            return (key, numbers);
        }).ToList();
    }

    public void SolvePartOne()
    {
        long result = _equations.Where(CheckEquation).Sum(x => x.key);
        Console.WriteLine(result);
    }

    public void SolvePartTwo()
    {
        _operators =
        [
            (a, b) => a + b,
            (a, b) => a * b,
            ConcatenateInt
        ];

        long result = _equations.Where(CheckEquation).Sum(x => x.key);
        Console.WriteLine(result);
    }

    private bool CheckEquation((long key, long[] numbers) equation)
    {
        var testValue = equation.key;
        var testNumbers = equation.numbers;

        int[] operatorSelector = new int[testNumbers.Length - 1];

        while (operatorSelector[0] <= _operators.Length - 1)
        {
            long result = testNumbers[0];
            for (int i = 1; i < testNumbers.Length; i++)
            {
                result = _operators[operatorSelector[i - 1]](result, testNumbers[i]);
            }

            if (result == testValue)
                return true;

            operatorSelector[operatorSelector.Length - 1]++;

            for (int i = operatorSelector.Length - 1; i > 0; i--)
            {
                if (operatorSelector[i] > _operators.Length - 1)
                {
                    operatorSelector[i] = 0;
                    operatorSelector[i - 1]++;
                }
            }
        }

        return false;
    }

    private long ConcatenateInt(long a, long b)
    {
        long bDigits = (long)Math.Log10(b) + 1;
        return a * (long)Math.Pow(10, bDigits) + b;
    }

}
