using System.Reflection;

namespace AdventOfCode.Infrastructure.Services;

public class LoadInputService
{
    public string GetInput(string inputDay, int year)
    {
        string assemblyLocation = Assembly.GetEntryAssembly().Location;
        string assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
        string path = Path.Combine(assemblyDirectory, "Years", year.ToString(), "Input", $"{inputDay}.txt");

        return File.ReadAllText(path);
    }

    public string[] GetInputAsLines(string inputDay, int year)
    {
        string assemblyLocation = Assembly.GetEntryAssembly().Location;
        string assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
        string path = Path.Combine(assemblyDirectory, "Years", year.ToString(), "Input", $"{inputDay}.txt");

        return File.ReadAllLines(path);
    }
}
