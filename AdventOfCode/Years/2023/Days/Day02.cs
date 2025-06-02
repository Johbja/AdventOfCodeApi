using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;
using AdventOfCode.Infrastructure.Attributes;

namespace AdventOfCode.Years._2023;

[DayInfo("Cube Conundrum", "Day 2")]
internal class Day02 : ISolution
{
    private readonly string[] _input;
    private readonly List<(int game, List<List<(int count, string type)>> sets)> games;

    public Day02(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day02), 2023);

        games = _input.Select(s => s.Split(":"))
        .Select
        (
            s =>
            (
                game: int.Parse(s[0].Split(" ")[1]),
                sets: s[1].Split(";")
                    .Select
                    (
                        sets => sets.Split(",")
                            .Select(set => set.Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToList()
                            .Select
                            (
                                cube =>
                                (
                                    count: int.Parse(cube[0]),
                                    type: cube[1]
                                )
                            )
                            .ToList()
                    )
                    .ToList()
              )
        )
        .ToList();
    }

    public void SolvePartOne()
    {
        //12 red cubes, 13 green cubes, and 14 blue cubes

        var impossibleGames = games.Where(game
            => game.sets.Where(sets
                => sets.Any(set => set.type == "red" && set.count > 12)
                    || sets.Any(set => set.type == "green" && set.count > 13)
                    || sets.Any(set => set.type == "blue" && set.count > 14))
                .Count() > 0)
            .Select(x => x.game)
            .ToList();

        var result = games.Where(x => !impossibleGames.Contains(x.game)).Sum(x => x.game);

        Console.WriteLine(result);
    }

    public void SolvePartTwo()
    {
        int sum = 0;
        
        foreach (var game in games)
        {
            int mred = 1;
            int mgreen = 1;
            int mblue = 1;

            foreach (var set in game.sets)
            {
                var reds = set.Where(x => x.type == "red");
                if (reds.Any())
                {
                    int credMax = reds.Max(x => x.count);
                    if(credMax >= mred)
                        mred = credMax;
                }

                var greens = set.Where(x => x.type == "green");
                if (greens.Any())
                {
                    int cgreenMax = greens.Max(x => x.count);
                    if (cgreenMax >= mgreen)
                        mgreen = cgreenMax;
                }

                var blues = set.Where(x => x.type == "blue");
                if (blues.Any())
                {
                    int cblueMax = blues.Max(x => x.count);
                    if (cblueMax >= mblue)
                        mblue = cblueMax;
                }
            }

            sum += mred * mgreen * mblue;
        }

        Console.WriteLine(sum);
    }

}
