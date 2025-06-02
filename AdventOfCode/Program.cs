using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;
using System.Reflection;

namespace AdventOfCode;

public class Program
{
    private static Dictionary<int, Type> _yearRepository = new();

    static void Main(string[] args)
    {
        //ConstructSolutionRepository();

        try
        {
            var menu = new MenuService();
            menu.Show();

            //if (int.TryParse(args[0], out int year))
            //{
            //    var currentyear = CreateInstanceOfYear(year);
            //    var currentDay = currentyear.CreateInstanceOfSolution(args[1]);
            //    currentyear.Solve(currentDay);
            //}
            //else
            //{
            //    Console.WriteLine("Could not parse input argument");
            //}
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void ConstructSolutionRepository()
    {
        Assembly currentAssembly = Assembly.GetExecutingAssembly();

        Type interfaceType = typeof(IAdventOfCode);

        _yearRepository = currentAssembly
            .GetTypes()
            .Where(type => type != null && interfaceType.IsAssignableFrom(type) && type != interfaceType)
            .OrderBy(type => type.Name)
            .ToDictionary(type => int.Parse(type.Name.Where(x => char.IsDigit(x)).Aggregate("", (a, b) => a + b.ToString())), type => type);
    }

    private static IAdventOfCode CreateInstanceOfYear(int year)
    {
        if (Activator.CreateInstance(_yearRepository[year]) is not IAdventOfCode instance)
            throw new ArgumentNullException(nameof(instance), $"instance of year {year} could not be created");

        return instance;
    }
}
