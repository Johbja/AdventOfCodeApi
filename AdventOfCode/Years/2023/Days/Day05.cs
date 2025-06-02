using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;
using AdventOfCode.Infrastructure.Attributes;
using System.Data;

namespace AdventOfCode.Years._2023;

[DayInfo("If You Give A Seed A Fertilizer", "Day 5")]
public class Day05 : ISolution
{
    private readonly string _input;
    private readonly long[] seeds;
    private readonly Dictionary<string, long[][]> maps;

    public Day05(LoadInputService inputService)
    {
        _input = inputService.GetInput(nameof(Day05), 2023);
        string[] sections = _input.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);
        seeds = sections[0]
            .Split(":")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();

        maps = sections.Skip(1).Select(section => section.Split(":"))
            .Select
            (
                map =>
                (
                    mapnName: map[0],
                    values: map[1].Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
                        .Select
                        (
                            row => row.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                            .Select(long.Parse)
                            .ToArray()
                        )
                        .ToArray()
                 )
            )
            .ToDictionary(key => key.mapnName, value => value.values);
    }

    public void SolvePartOne()
    {
        long[] seedsCopy = new long[seeds.Length];

        Array.Copy(seeds, seedsCopy, seeds.Length);

        long result = CalculateMinSeedLocation(seedsCopy);
        Console.WriteLine(result);
    }

    public void SolvePartTwo()
    {
        var srcPairs = seeds.Chunk(2).ToList();
        var seedRanges = srcPairs.Select(pair => (start: pair[0], end: pair[0] + pair[1])).ToArray();

        long min = long.MaxValue;

        List<int> lockObject = new();

        List<Task> threads = new();

        foreach (var (start, end) in seedRanges)
        {
            threads.Add(Task.Run(() =>
            {
                long[] currendSeed = new long[end - start];

                for (long i = start; i < end; i++)
                {
                    currendSeed[i - start] = i;
                }

                long currenMin = CalculateMinSeedLocation(currendSeed);

                lock (lockObject)
                {
                    if (min > currenMin)
                        min = currenMin;
                }
            }));
        }

        Console.WriteLine($"Running {threads.Count} threads, wait wile they finnish");
        
        Task.WhenAll(threads).Wait();

        Console.WriteLine($"Min location: {min}");
    }


    private long CalculateMinSeedLocation(long[] currentSrc)
    {
        foreach (var key in maps.Keys)
        {

            long len = currentSrc.Length;
            for (long i = 0; i < len; i++)
            {
                var validMaps = maps[key].Where(row => currentSrc[i] >= row[1] && currentSrc[i] < (row[1] + row[2])).ToList();

                if (!validMaps.Any())
                {
                    continue;
                }

                var validMap = validMaps.Single();

                var offset = Math.Abs((validMap[1] - currentSrc[i]));
                currentSrc[i] = validMap[0] + offset;

            }
        }

        var result = currentSrc.Min();
        return result;
    }
}

