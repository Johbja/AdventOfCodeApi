using AdventOfCode.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Infrastructure.Services;
public class MenuService
{
    private readonly Dictionary<int, Type> _yearRepository = [];

    public MenuService()
    {
        Assembly currentAssembly = Assembly.GetEntryAssembly();

        Type interfaceType = typeof(IAdventOfCode);

        _yearRepository = currentAssembly
            .GetTypes()
            .Where(type => type != null && interfaceType.IsAssignableFrom(type) && type != interfaceType)
            .OrderBy(type => type.Name)
            .ToDictionary(type => int.Parse(type.Name.Where(x => char.IsDigit(x)).Aggregate("", (a, b) => a + b.ToString())), type => type);
    }

    public void Show()
    {
        Console.Clear();
        Console.WriteLine("---------------------------");
        Console.WriteLine("--AdventOfCode Repository--");
        Console.WriteLine("---------------------------");

        foreach (var pair in _yearRepository)
        {
            Console.WriteLine(pair.Value.Name.Where(char.IsDigit).Aggregate("", (a,b) => a + b));
        }

        Console.WriteLine();
        Console.Write("Select year: ");
        var result = Console.ReadLine();

        string? input = result?.Split(":", StringSplitOptions.RemoveEmptyEntries).Last();

        if (int.TryParse(input, out int year) && _yearRepository.ContainsKey(year))
            Navigate(year);
        else
            Show();
    }

    public void Navigate(int year)
    {
        var currentyear = CreateInstanceOfYear(year);
        Console.Clear();
        currentyear.PrintRepo();

        Console.Write("Select day: ");
        var result = Console.ReadLine();

        string? input = result?.Split(":", StringSplitOptions.RemoveEmptyEntries).Last();

        var currentDay = currentyear.CreateInstanceOfSolution(input);
        if(currentDay is not null)
        {
            Console.Clear();
            currentyear.Solve(currentDay);

            Console.ReadKey();
            Show();
        }

        Navigate(year);
    }

    private IAdventOfCode CreateInstanceOfYear(int year)
    {
        if (Activator.CreateInstance(_yearRepository[year]) is not IAdventOfCode instance)
            throw new ArgumentNullException(nameof(instance), $"instance of year {year} could not be created");

        return instance;
    }

}
