using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;
using AdventOfCode.Infrastructure.Attributes;
using System.Data;

namespace AdventOfCode.Years._2023;

[DayInfo("Parabolic Reflector Dish", "Day 14")]
public class Day14 : ISolution
{

    private readonly string[] _input;

    public Day14(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day14), 2023);
    }

    public void SolvePartOne()
    {
        var platform = _input.Select(x => x.Select(c => c).ToArray()).ToArray();

        for (int column = 0; column < platform[0].Length; column++)
        {
            var rocksToMove = platform.Select((cRow, index) => (x: cRow, index)).Where(x => x.x[column] == 'O');

            foreach (var rock in rocksToMove)
            {
                var closestObsticle = platform
                    .Select((cRow, index) => (cRow, index)).Where(x => x.index < rock.index && (x.cRow[column] == 'O' || x.cRow[column] == '#'))
                    .OrderBy(x => x.index)
                    .DefaultIfEmpty((new[] { ' ' }, index: -1))
                    .Last();

                platform[rock.index][column] = '.';

                if (closestObsticle.index == -1)
                {
                    platform[0][column] = 'O';
                    continue;
                }

                platform[closestObsticle.index + 1][column] = 'O';
            }
        }

        var result = platform.Select((x, i) => (count: x.Where(c => c == 'O').Count(), i)).Sum(x => (platform.Length - x.i) * x.count);

        Console.WriteLine(result);
    }

    public void SolvePartTwo()
    {
        var platform = _input.Select(x => x.Select(c => c).ToArray()).ToArray();

        //Dictionary<int, List<int>> stateToIterationMap = new();
        List<int> seq = new();

        int goalIteration = 1000000000;

        for (int i = 0; i < 10000; i++)
        {
            platform = RunCycle(platform);

            var result = platform.Select((x, i) => (count: x.Where(c => c == 'O').Count(), i)).Sum(x => (platform.Length - x.i) * x.count);

            seq.Add(result);

            //if(!stateToIterationMap.ContainsKey(result))
            //    stateToIterationMap.Add(result, new List<int>());

            //stateToIterationMap[result].Add(i);
        }

        var seqIdentifier = seq.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Select(x => x).ToList());

        //var seqIdentifierIndecies = seq.Select((v, i) => (v, i)).Where(x => x.v == seqIdentifier).Select(x => x.i);

        //int start = 0;
        //var subSeqs = seqIdentifierIndecies.Select(x =>
        //{
        //    var subSeq = seq.GetRange(start, x - start + 1);
        //    start = x + 1;
        //    return subSeq;
        //}).ToList();

        //int repeater = -1;
        //if(subSeqs.Count() > 1 && subSeqs[0].Count == subSeqs[1].Count)
        //{
        //    repeater = subSeqs.Count;
        //    int remaningIteration = goalIteration % repeater;
        //}


        //platform.ToList().ForEach(Console.WriteLine);

        //var result = platform.Select((x, i) => (count: x.Where(c => c == 'O').Count(), i)).Sum(x => (platform.Length - x.i) * x.count);

        Console.WriteLine("");
    }

    private char[][] RunCycle(char[][] platform)
    {
        //N --> W --> S --> E

        //N
        for(int column = 0; column < platform[0].Length; column++)
        {
            var rocksToMove = platform.Select((cRow, index) => (x: cRow, index)).Where(x => x.x[column] == 'O');

            foreach(var rock in rocksToMove)
            {
                var closestObsticle = platform
                    .Select((cRow, index) => (cRow, index)).Where(x => x.index < rock.index && (x.cRow[column] == 'O' || x.cRow[column] == '#'))
                    .OrderBy(x => x.index)
                    .DefaultIfEmpty((new[] { ' ' }, index: -1))
                    .Last();

                platform[rock.index][column] = '.';

                if(closestObsticle.index == -1)
                {
                    platform[0][column] = 'O';
                    continue;
                }

                platform[closestObsticle.index + 1][column] = 'O';
            }
        }

        //W
        for (int row = 0; row < platform.Length; row++)
        {
            var rocksToMove = platform[row]
                .Select((value, index) => (value, index))
                .Where(x => x.value == 'O')
                .OrderBy(x => x.index);

            foreach (var rock in rocksToMove)
            {
                var closestObsticle = platform[row]
                .Select((value, index) => (value, index))
                .Where(x => (x.value == 'O' || x.value == '#') && x.index < rock.index)
                .OrderBy(x => x.index)
                .DefaultIfEmpty((value: ' ', index: -1))
                .Last();

                platform[row][rock.index] = '.';

                if (closestObsticle.index == -1)
                {
                    platform[row][0] = 'O';
                    continue;
                }

                platform[row][closestObsticle.index + 1] = 'O';
            }
        }

        //S
        for (int column = 0; column < platform.Length; column++)
        {
            var rocksToMove = platform.Select((cRow, index) => (x: cRow, index))
                .Where(x => x.x[column] == 'O')
                .OrderByDescending(x => x.index);

            foreach (var rock in rocksToMove)
            {
                var closestObsticle = platform
                    .Select((cRow, index) => (cRow, index)).Where(x => x.index > rock.index && (x.cRow[column] == 'O' || x.cRow[column] == '#'))
                    .OrderBy(x => x.index)
                    .DefaultIfEmpty((new[] { ' ' }, index: -1))
                    .First();

                platform[rock.index][column] = '.';

                if (closestObsticle.index == -1)
                {
                    platform[^1][column] = 'O';
                    continue;
                }

                platform[closestObsticle.index - 1][column] = 'O';
            }
        }

        //E
        for (int row = platform.Length - 1; row >= 0; row--)
        {
            var rocksToMove = platform[row]
                .Select((value, index) => (value, index))
                .Where(x => x.value == 'O')
                .OrderByDescending(x => x.index);

            foreach (var rock in rocksToMove)
            {
                var closestObsticle = platform[row]
                .Select((value, index) => (value, index))
                .Where(x => (x.value == 'O' || x.value == '#') && x.index > rock.index)
                .OrderBy(x => x.index)
                .DefaultIfEmpty((value: ' ', index: -1))
                .First();

                platform[row][rock.index] = '.';

                if (closestObsticle.index == -1)
                {
                    platform[row][^1] = 'O';
                    continue;
                }

                platform[row][closestObsticle.index - 1] = 'O';
            }
        }

        return platform;
    }

}
