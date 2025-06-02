using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Attributes;
using AdventOfCode.Infrastructure.Services;
using System.Reflection;

namespace AdventOfCode.Years._2024;
public class AdventOfCode2024 : IAdventOfCode
{
    private readonly string year = "2024";
    private readonly Dictionary<int, Type> _solutionRepository = [];

    public AdventOfCode2024()
    {
        Assembly currentAssembly = Assembly.GetExecutingAssembly();

        Type interfaceType = typeof(ISolution);

        _solutionRepository = currentAssembly
            .GetTypes()
            .Where(type =>
                type != null
                && interfaceType.IsAssignableFrom(type)
                && type != interfaceType
                && type.Namespace.StartsWith("AdventOfCode.Years._2024"))
            .OrderBy(type => type.Name)
            .ToDictionary(type => int.Parse(type.Name.Where(x => char.IsDigit(x)).Aggregate("", (a, b) => a + b.ToString())), type => type);
    }

    public ISolution? CreateInstanceOfSolution(string arg)
    {
        if (int.TryParse(arg, out int day) && _solutionRepository.TryGetValue(day, out var solution))
        {
            return (ISolution)Activator.CreateInstance(_solutionRepository[day], new LoadInputService());
        }

        return null;
    }

    public void Solve(ISolution solution)
    {
        DayInfo? dayInfo = solution.GetType().GetCustomAttribute<DayInfo>();
        string displayName = "";

        if (dayInfo is not null)
        {
            displayName = $"Year:{year} {dayInfo.Day}: {dayInfo.SolutionName}";
            Console.WriteLine(displayName);
            PrintSeparetor(displayName.Length);
        }

        solution.SolvePartOne();
        PrintSeparetor(displayName.Length);

        solution.SolvePartTwo();
        PrintSeparetor(displayName.Length);
    }

    public void PrintSeparetor(int length)
    {
        Console.WriteLine(new string('-', length));
    }

    public void PrintRepo()
    {
        foreach (var pair in _solutionRepository)
        {
            var dayinfo = pair.Value.GetCustomAttribute<DayInfo>();
            if (dayinfo is not null)
            {
                Console.WriteLine($"{dayinfo.Day}: {dayinfo.SolutionName}");
            }
        }
    }
}
