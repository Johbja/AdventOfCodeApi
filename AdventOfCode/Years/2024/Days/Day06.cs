using AdventOfCode.Infrastructure.Attributes;
using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;
using AdventOfCode.Infrastructure.Models._2024.Day06;
using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode.Years._2024.Days;

[DayInfo("Guard Gallivant", "Day 6")]
public class Day06 : ISolution
{
    private readonly string[] _input;

    public Day06(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day06), 2024);
    }

    private int EvaluateCharacter(char value)
        => value == '^' ? 2 : (value == '#' ? 1 : 0);

    public void SolvePartOne()
    {
        var grid = _input.Select(row => row.Select(col => EvaluateCharacter(col)).ToArray()).ToArray();
        var guardPos = grid.SelectMany((row, y) => row.Select((value, x) => (x, y, value)))
                          .Where(x => x.value == 2)
                          .Select(pos => (pos.x, pos.y))
                          .Single();

        grid[guardPos.y][guardPos.x] = 0;

        int[] yMoveset = [-1, 0, 1, 0];
        int[] xMoveset = [0, 1, 0, -1];

        Direction currentDirection = Direction.Up;
        Dictionary<(int x, int y), int> visitedPositions = new()
        {
            { guardPos , 1}
        };

        while (guardPos.x > 0 && guardPos.x < grid[0].Length && guardPos.y > 0 && guardPos.y < grid.Length)
        {
            var (x, y) = (guardPos.x + xMoveset[(int)currentDirection], guardPos.y + yMoveset[(int)currentDirection]);

            if (x >= 0 && x < grid[0].Length && y >= 0 && y < grid.Length && grid[y][x] == 1)
            {
                currentDirection = (Direction)(((int)currentDirection + 1) % 4);
            }
            else
            {
                guardPos = (x, y);
            }

            if (x >= 0 && x < grid[0].Length && y >= 0 && y < grid.Length)
            {
                visitedPositions.TryAdd(guardPos, 0);
                visitedPositions[guardPos]++;
            }
        }

        Console.WriteLine(visitedPositions.Count);
    }

    public void SolvePartTwo()
    {
        var grid = _input.Select(row => row.Select(col => EvaluateCharacter(col)).ToArray()).ToArray();
        var guardPos = grid.SelectMany((row, y) => row.Select((value, x) => (x, y, value)))
                          .Where(x => x.value == 2)
                          .Select(pos => (pos.x, pos.y))
                          .Single();

        grid[guardPos.y][guardPos.x] = 0;

        int[] yMoveset = [-1, 0, 1, 0];
        int[] xMoveset = [0, 1, 0, -1];
        string[] directions = Enum.GetNames(typeof(Direction));

        Direction currentDirection = Direction.Up;
        HashSet<(int x, int y, int dir)> visitedStates = [];
        List<string> path = [];

        int cycleCounter = 0;
        for (int row = 0; row < grid.Length; row++)
        {
            for (int col = 0; col < grid[row].Length; col++)
            {
                if (row == guardPos.y && col == guardPos.x)
                    continue;

                int orgValue = grid[row][col];
                grid[row][col] = 1;

                while (guardPos.x > 0 && guardPos.x < grid[0].Length && guardPos.y > 0 && guardPos.y < grid.Length)
                {
                    var state = (guardPos.x, guardPos.y, dir:(int)currentDirection);

                    if (visitedStates.Any(oldState => oldState.x == state.x && oldState.y == state.y && oldState.dir == state.dir) 
                        && FindCycles(string.Join(",", path)))
                    {
                        cycleCounter++;
                        break;
                    }

                    visitedStates.Add(state);
                    path.Add(currentDirection.ToString());

                    var (x, y) = (guardPos.x + xMoveset[(int)currentDirection], guardPos.y + yMoveset[(int)currentDirection]);

                    if (x >= 0 && x < grid[0].Length && y >= 0 && y < grid.Length && grid[y][x] == 1)
                    {
                        currentDirection = (Direction)(((int)currentDirection + 1) % 4);
                    }
                    else
                    {
                        guardPos = (x, y);
                    }
                }

                grid[row][col] = orgValue;
            }
        }

        Console.WriteLine(cycleCounter);
    }

    private bool FindCycles(string input)
    {
        string[] words = input.Split(',');
        int n = words.Length;

        // Create a concatenated version of the input
        string[] doubleWords = words.Concat(words).ToArray();
        int[] lps = new int[doubleWords.Length];

        // KMP preprocessing
        int j = 0;
        for (int i = 1; i < doubleWords.Length; i++)
        {
            while (j > 0 && !doubleWords[i].Equals(doubleWords[j]))
                j = lps[j - 1];

            if (doubleWords[i].Equals(doubleWords[j]))
                lps[i] = ++j;
        }

        // Find repeating pattern
        int patternLength = n - lps[n - 1];
        if (n % patternLength == 0)
        {
            return true;
        }

        return false;
    }

}
